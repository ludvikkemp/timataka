using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.HomeViewModels;
using Timataka.Core.Models.ViewModels.AdminViewModels;
using Timataka.Core.Models.ViewModels.ResultViewModels;
using Timataka.Core.Models.ViewModels.ChipViewModels;
using Timataka.Core.Models.ViewModels.UserViewModels;

namespace Timataka.Core.Data.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private bool _disposed = false;
        private readonly ApplicationDbContext _db;
        private readonly TimingDbContext _tdb;

        public ResultRepository(ApplicationDbContext db, TimingDbContext tdb)
        {
            _db = db;
            _tdb = tdb;
        }

        public Result Add(Result r)
        {
            _db.Results.Add(r);
            _db.SaveChanges();
            return r;
        }

        public async Task<Result> AddAsync(Result r)
        {
            await _db.Results.AddAsync(r);
            await _db.SaveChangesAsync();
            return r;
        }

        public bool Edit(Result r)
        {
            bool result = _db.Results.Update(r) != null;
            _db.SaveChanges();
            return result;
        }

        public async Task<bool> EditAsync(Result r)
        {
            bool result = _db.Results.Update(r) != null;
            await _db.SaveChangesAsync();
            return result;
        }

        public IEnumerable<Result> Get()
        {
            return _db.Results.ToList();
        }

        public Result GetByUserIdAndHeatId(string userId, int heatId)
        {
            var result = (from x in _db.Results
                          where x.UserId == userId && x.HeatId == heatId
                          select x).SingleOrDefault();
            return result;
        }

        public async Task<Result> GetByUserIdAndHeatIdAsync(string userId, int heatId)
        {
            var result = await (from x in _db.Results
                                where x.UserId == userId && x.HeatId == heatId
                                select x).SingleOrDefaultAsync();
            return result;
        }

        public bool Remove(Result r)
        {
            var result = false;
            if (_db.Results.Remove(r) != null)
            {
                result = true;
            }
            _db.SaveChanges();
            return result;
        }

        public async Task<bool> RemoveAsync(Result r)
        {
            var result = false;
            if (_db.Results.Remove(r) != null)
            {
                result = true;
            }
            await _db.SaveChangesAsync();
            return result;
        }

        DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            return new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind);
        }

        public IEnumerable<ResultViewModel> GetResultViewModelsForEvent(int eventId)
        {
            var results = (from r in _db.Results
                               //join u in _db.Users on r.UserId equals u.Id
                               join h in _db.Heats on r.HeatId equals h.Id
                               join u in _db.ChipsInHeats on r.UserId equals u.UserId
                               join user in _db.Users on u.UserId equals user.Id
                               join c in _db.ContestantsInHeats on u.UserId equals c.UserId
                               where h.EventId == eventId && u.HeatId == h.Id && c.HeatId == h.Id
                               select new ResultViewModel
                               { 
                                   Club = r.Club,
                                   Country = r.Country,
                                   Created = r.Created,
                                   RawGunTime = 0,
                                   GunTime = r.FinalTime,
                                   ChipTime = "",
                                   Gender = r.Gender,
                                   HeatId = r.HeatId,
                                   HeatNumber = h.HeatNumber,
                                   Modified = r.Modified,
                                   Name = r.Name,
                                   Nationality = r.Nationality,
                                   Notes = r.Notes,
                                   Status = r.Status,
                                   UserId = r.UserId,
                                   ChipCode = u.ChipCode,
                                   Bib = c.Bib,
                                   DateOfBirth = user.DateOfBirth
                               }).ToList();

            foreach(var result in results)
            {
                int rawTime = CalculateGuntime(result.HeatId, result.ChipCode);
                result.RawGunTime = rawTime;
                TimeSpan finalTime = TimeSpan.FromMilliseconds(rawTime);
                if (rawTime % 1000 != 0)
                {
                    finalTime += TimeSpan.FromSeconds(1);
                }
                if (rawTime == 0)
                {
                    result.GunTime = "";
                }
                else
                {
                    result.GunTime = finalTime.ToString(@"hh\:mm\:ss");
                }

                rawTime = CalculateChipTime(result.HeatId, result.ChipCode);
                TimeSpan chipTime = TimeSpan.FromMilliseconds(rawTime);
                if (rawTime % 1000 != 0)
                {
                    chipTime += TimeSpan.FromSeconds(1);
                }
                if (rawTime == 0)
                {
                    result.GunTime = "";
                }
                else
                {
                    result.ChipTime = chipTime.ToString(@"hh\:mm\:ss");
                }
            }
            return results.OrderBy(o => o.RawGunTime);
        }

        public IEnumerable<MyResultsViewModel> GetResultsForUser(string userId)
        {
            var results = (from r in _db.Results
                           join h in _db.Heats on r.HeatId equals h.Id
                           join u in _db.Users on r.UserId equals u.Id
                           join e in _db.Events on h.EventId equals e.Id
                           join i in _db.CompetitionInstances on e.CompetitionInstanceId equals i.Id
                           join d in _db.Disciplines on e.DisciplineId equals d.Id
                           join c in _db.Courses on e.CourseId equals c.Id
                           join ch in _db.ChipsInHeats on u.Id equals ch.UserId
                           join con in _db.ContestantsInHeats on u.Id equals con.UserId
                               where u.Id == userId && h.Id == ch.HeatId && con.HeatId == h.Id
                               select new MyResultsViewModel
                               {
                                   CourseId = c.Id,
                                   Club = r.Club,
                                   CompetitionInstanceId = i.Id,
                                   CompetitionInstanceName = i.Name,
                                   CompetitionInstanceStatus = i.Status,
                                   HeatId = h.Id,
                                   Status = r.Status,
                                   Country = r.Country,
                                   CourseDistance = c.Distance,
                                   CourseName = c.Name,
                                   Created = r.Created,
                                   DisciplineId = d.Id,
                                   DisciplineName = d.Name,
                                   EventDateFrom = e.DateFrom,
                                   EventDateTo = e.DateTo,
                                   EventId = e.Id,
                                   EventName = e.Name,
                                   GunTime = r.FinalTime,
                                   ChipTime = "",
                                   Gender = r.Gender,
                                   HeatNumber = h.HeatNumber,
                                   Modified = r.Modified,
                                   Nationality = r.Nationality,
                                   Notes = r.Notes,
                                   SportId = d.SportId,
                                   UserId = u.Id,
                                   UserName = u.FirstName + " " + u.MiddleName + " " + u.LastName,
                                   ChipCode = ch.ChipCode,
                                   Bib = con.Bib,
                                   DateOfBirth = u.DateOfBirth
                               }).Distinct().ToList();

            foreach (var result in results)
            {
                int rawTime = CalculateGuntime(result.HeatId, result.ChipCode);
                TimeSpan finalTime = TimeSpan.FromMilliseconds(rawTime);
                if (rawTime % 1000 != 0)
                {
                    finalTime += TimeSpan.FromSeconds(1);
                }
                if(rawTime == 0)
                {
                    result.GunTime = "";
                }
                else
                {
                    result.GunTime = finalTime.ToString(@"hh\:mm\:ss");
                }
                
                rawTime = CalculateChipTime(result.HeatId, result.ChipCode);
                TimeSpan chipTime = TimeSpan.FromMilliseconds(rawTime);
                if (rawTime % 1000 != 0)
                {
                    chipTime += TimeSpan.FromSeconds(1);
                }
                if (rawTime == 0)
                {
                    result.GunTime = "";
                }
                else
                {
                    result.ChipTime = chipTime.ToString(@"hh\:mm\:ss");
                }
            }

            return results;
        }

        public int CalculateGuntime(int heatId, string chipCode)
        {
            var guntime = (from mih in _db.MarkersInHeats
                             where mih.HeatId == heatId
                             join m in _db.Markers on mih.MarkerId equals m.Id
                             where m.Type == Models.Entities.Type.Gun
                             select m.Time).SingleOrDefault();
            if(guntime == 0)
            {
                return 0;
            }
            var finishtime = (from t in _db.Times
                where t.ChipCode == chipCode && t.HeatId == heatId && t.TimeNumber == 2
                select t.RawTime).SingleOrDefault();

            return finishtime - guntime;
        }

        public int CalculateChipTime(int heatId, string chipCode)
        {
            var chiptime = (from t in _db.Times
                            where t.ChipCode == chipCode && t.HeatId == heatId && t.TimeNumber == 1
                            select t.RawTime).SingleOrDefault();
            var finishtime = (from t in _db.Times
                           where t.ChipCode == chipCode && t.HeatId == heatId && t.TimeNumber == 2
                           select t.RawTime).SingleOrDefault();
            if(finishtime == 0 || chiptime == 0)
            {
                return 0;
            }
            return finishtime - chiptime;
        }

        /// <summary>
        /// Get all results from TimingDB
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RawResultViewModel> GetResultsFromTimingDb()
        {
            var result = (from r in _tdb.Results
                          join c in _tdb.Chips on r.Pid equals c.Pid
                          select new RawResultViewModel
                          {
                              ChipCode = c.Chip,
                              CompetitionInstanceId = r.CompetitionInstanceId,
                              Time01 = r.Time01 == null ? 0 : (int)r.Time01,
                              Time02 = r.Time02 == null ? 0 : (int)r.Time02
                          }).ToList();
            return result;
        }

      

        public Boolean AddTime(Time time)
        {
            var result = false;
            if (_db.Times.Add(time) != null)
            {
                result = true;
            }
            _db.SaveChanges();
            return result;
        }

        /// <summary>
        /// Return list of heats in an instance
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Heat> GetHeatsInCompetitionInstance(int id)
        {
            var result = (from i in _db.CompetitionInstances
                          where i.Id == id
                          join e in _db.Events on i.Id equals e.CompetitionInstanceId
                          join h in _db.Heats on e.Id equals h.EventId
                          select h).ToList();
            return result;
        }

        public IEnumerable<ChipInHeatViewModel> GetChipsInHeat(int heatId)
        {
            var chipsInHeat = (from c in _db.ChipsInHeats
                               join h in _db.Heats on c.HeatId equals h.Id
                               join chips in _db.Chips on c.ChipCode equals chips.Code
                               join i in _db.CompetitionInstances on chips.LastCompetitionInstanceId equals i.Id
                               join u in _db.Users on chips.LastUserId equals u.Id
                               select new ChipInHeatViewModel
                               {
                                   LastUserId = u.Id,
                                   LastCompetitionInstanceId = i.Id,
                                   Active = chips.Active,
                                   ChipCode = chips.Code,
                                   LastCompetitionInstanceName = i.Name,
                                   LastSeen = chips.LastSeen,
                                   LastUserName = u.FirstName + " " + u.MiddleName + " " + u.LastName,
                                   LastUserSsn = u.Ssn,
                                   Number = chips.Number,
                                   HeatId = h.Id,
                                   HeatNumber = h.HeatNumber,
                                   Valid = c.Valid,
                                   UserId = c.UserId
                               }).ToList();

            return chipsInHeat;
        }

        public Time GetTime(int heatId, string chipCode, int timeNumber)
        {
            return (from t in _db.Times
                   where t.HeatId == heatId && t.ChipCode == chipCode && t.TimeNumber == timeNumber
                   select t).SingleOrDefault();
        }

        public Boolean Remove(int heatId, string chipCode, int timeNumber)
        {
            Boolean result = false;
            var time = GetTime(heatId, chipCode, timeNumber);
            if (_db.Times.Remove(time) != null)
            {
                result = true;
            }
            _db.SaveChanges();
            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }

            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
