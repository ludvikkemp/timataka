using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    
    public class DisciplineRepository : IDisciplineRepository
    {
        private bool _disposed = false;
        private readonly ApplicationDbContext _db;

        public DisciplineRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Discipline> Get()
        {
            return _db.Disciplines.ToList();
        }

        public Discipline GetById(int id)
        {
            return _db.Disciplines.SingleOrDefault(x => x.Id == id);
        }

        public Task<Discipline> GetByIdAsync(int id)
        {
            return _db.Disciplines.SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Insert(Discipline entity)
        {
            _db.Disciplines.Add(entity);
            _db.SaveChanges();
        }

        public async Task InsertAsync(Discipline entity)
        {
            _db.Disciplines.Add(entity);
            await _db.SaveChangesAsync();
        }

        public void Edit(Discipline entity)
        {
            _db.Disciplines.Update(entity);
            _db.SaveChanges();
        }

        public async Task EditAsync(Discipline entity)
        {
            _db.Disciplines.Update(entity);
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

        public void Remove(Discipline entity)
        {
            //TODO:Mark as removed, not delete compleatly
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Task<Discipline> entity)
        {
            //TODO:Mark as removed, not delete compleatly
            throw new NotImplementedException();
        }

        public List<Sport> GetSports()
        {
            return _db.Sports.OrderBy(x => x.Name).ToList();
        }
    }
}
