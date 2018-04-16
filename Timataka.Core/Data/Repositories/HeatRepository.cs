using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.HeatViewModels;

namespace Timataka.Core.Data.Repositories
{
    public class HeatRepository : IHeatRepository
    {
        private bool _disposed = false;
        private readonly ApplicationDbContext _db;

        public HeatRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Edit(Heat h)
        {
            _db.Heats.Update(h);
            _db.SaveChanges();
        }

        public async Task EditAsync(Heat h)
        {
            _db.Heats.Update(h);
            await _db.SaveChangesAsync();
        }

        public IEnumerable<Heat> Get()
        {
            return _db.Heats.ToList();
        }

        public Heat GetById(int id)
        {
            return _db.Heats.SingleOrDefault(x => x.Id == id);
        }

        public Task<Heat> GetByIdAsync(int id)
        {
            return _db.Heats.SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Insert(Heat h)
        {
            _db.Heats.Add(h);
            _db.SaveChanges();
        }

        public async Task InsertAsync(Heat h)
        {
            await _db.Heats.AddAsync(h);
            await _db.SaveChangesAsync();
        }

        public void Remove(Heat h)
        {
            h.Deleted = true;
            _db.Heats.Update(h);
            _db.SaveChanges();
        }

        public async Task RemoveAsync(Heat h)
        {
            h.Deleted = true;
            _db.Heats.Update(h);
            await _db.SaveChangesAsync();
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

        public IEnumerable<ContestantInHeatViewModel> GetContestantsInHeat(int heatId)
        {
            var contestants = (from c in _db.ContestantsInHeats
                               join u in _db.Users on c.UserId equals u.Id
                               join h in _db.Heats on heatId equals h.Id
                               where c.HeatId == heatId
                               select new ContestantInHeatViewModel
                               {
                                   Bib = c.Bib,
                                   DateOfBirth = u.DateOfBirth,
                                   Name = u.FirstName + " " + u.LastName,
                                   Gender = u.Gender,
                                   HeatId = h.Id,
                                   HeatNumber = h.HeatNumber,
                                   Phone = u.Phone,
                                   Ssn = u.Ssn,
                                   Team = c.Team,
                                   UserId = u.Id
                               }).ToList();
            return contestants;
        }

        public IEnumerable<ApplicationUser> GetApplicationUsersInHeat(int id)
        {
            var users = (from c in _db.ContestantsInHeats
                               join u in _db.Users on c.UserId equals u.Id                              
                               select u).ToList();
            return users;
        }

        public ContestantInHeat GetContestantInHeatById(int heatId, string userId)
        {
            var contestantInHeat = (from c in _db.ContestantsInHeats
                                    where c.HeatId == heatId && c.UserId == userId
                                    select c).SingleOrDefault();
            return contestantInHeat;
        }

        public void EditContestantInHeat(ContestantInHeat h)
        {
            _db.ContestantsInHeats.Update(h);
            _db.SaveChanges();
        }
        public async Task EditAsyncContestantInHeat(ContestantInHeat h)
        {
            _db.ContestantsInHeats.Update(h);
            await _db.SaveChangesAsync();
        }

        public void RemoveContestantInHeat(ContestantInHeat h)
        {
            _db.ContestantsInHeats.Remove(h);
            _db.SaveChanges();
        }
        public async Task RemoveAsyncContestantInHeat(ContestantInHeat h)
        {
            _db.ContestantsInHeats.Remove(h);
            await _db.SaveChangesAsync();
        }

        public void InsertContestantInHeat(ContestantInHeat h)
        {
            _db.ContestantsInHeats.Add(h);
            _db.SaveChanges();
        }
        public async Task InsertAsyncContestantInHeat(ContestantInHeat h)
        {
            _db.ContestantsInHeats.Add(h);
            await _db.SaveChangesAsync();
        }
    }
}
