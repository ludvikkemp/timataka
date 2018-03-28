using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.AdminViewModels;

namespace Timataka.Core.Data.Repositories
{
    
    public class SportRepository : ISportRepository
    {
        private bool _disposed = false;
        private readonly ApplicationDbContext _db;

        public SportRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Sport> Get()
        {
            return _db.Sports.ToList();
        }

        public IEnumerable<SportsViewModel> GetListOfSportsViewModels()
        {
            var result = (from s in _db.Sports
                select new SportsViewModel
                {
                    Name = s.Name,
                    Id = s.Id
                }).ToList();
            return result;
        }

        public Sport GetById(int id)
        {
            return _db.Sports.SingleOrDefault(x => x.Id == id);
        }

        public Task<Sport> GetByIdAsync(int id)
        {
            return _db.Sports.SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Insert(Sport entity)
        {
            _db.Sports.Add(entity);
            _db.SaveChanges();
        }

        public async Task InsertAsync(Sport entity)
        {
            await _db.Sports.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public void Edit(Sport entity)
        {
            _db.Sports.Update(entity);
            _db.SaveChanges();
        }

        public async Task EditAsync(Sport entity)
        {
            _db.Sports.Update(entity);
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

        public void Remove(Sport entity)
        {
            //TODO:Mark as removed, not delete compleatly
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Sport entity)
        {
            //TODO:Mark as removed, not delete compleatly
            throw new NotImplementedException();
        }

        public Task<Sport> GetSportByNameAsync(string sportName)
        {
            var s = _db.Sports.SingleOrDefaultAsync(x => x.Name == sportName);
            return s;
        }
    }
}
