using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.HomeViewModels;
using Timataka.Core.Models.ViewModels.AdminViewModels;


namespace Timataka.Core.Data.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private bool _disposed = false;
        private readonly ApplicationDbContext _db;

        public ResultRepository(ApplicationDbContext db)
        {
            _db = db;
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
            var result = false;
            if (_db.Results.Update(r) != null)
            {
                result = true;
            }
            _db.SaveChanges();
            return result;
        }

        public async Task<bool> EditAsync(Result r)
        {
            var result = false;
            if (_db.Results.Update(r) != null)
            {
                result = true;
            }
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

        public IEnumerable<ResultViewModel> GetResultViewModelsForEvent(int eventId)
        {
            var results = (from r in _db.Results
                               join u in _db.Users on r.UserId equals u.Id
                               join h in _db.Heats on r.HeatId equals h.Id
                               where h.EventId == eventId
                               select new ResultViewModel
                               {
                                   Club = r.Club,
                                   Country = r.Country,
                                   Created = r.Created,
                                   FinalTime = r.FinalTime,
                                   Gender = r.Gender,
                                   HeatId = r.HeatId,
                                   HeatNumber = h.HeatNumber,
                                   Modified = r.Modified,
                                   Name = r.Name,
                                   Nationality = r.Nationality,
                                   Notes = r.Notes,
                                   Status = r.Status,
                                   UserId = r.UserId
                               }).ToList();
            return results;
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
