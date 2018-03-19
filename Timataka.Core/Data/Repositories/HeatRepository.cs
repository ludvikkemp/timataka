using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;

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

        public IEnumerable<ContestantInHeat> GetContestantsInHeat(int heatId)
        {
            var contestants = (from c in _db.ContestantsInHeats
                               where c.HeatId == heatId
                               select c).ToList();
            return contestants;
        }

        public IEnumerable<ApplicationUser> GetApplicationUsersInHeat(int id)
        {
            var users = (from c in _db.ContestantsInHeats
                               join u in _db.Users on c.UserId equals u.Id                              
                               select u).ToList();
            return users;
        }
    }
}
