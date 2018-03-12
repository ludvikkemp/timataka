using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    
    public class SportRepository : ISportRepository
    {
        private bool _disposed = false;
        private readonly ApplicationDbContext _context;

        public SportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Sport> Get()
        {
            return _context.Sports.ToList();
        }

        public Sport GetById(int id)
        {
            return _context.Sports.SingleOrDefault(x => x.Id == id);
        }

        public Task<Sport> GetByIdAsync(int id)
        {
            return _context.Sports.SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Insert(Sport entity)
        {
            _context.Sports.Add(entity);
            _context.SaveChanges();
        }

        public async Task InsertAsync(Sport entity)
        {
            await _context.Sports.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Edit(Sport entity)
        {
            _context.Sports.Update(entity);
            _context.SaveChanges();
        }

        public async Task EditAsync(Sport entity)
        {
            _context.Sports.Update(entity);
            await _context.SaveChangesAsync();
        }

        

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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
            var s = _context.Sports.SingleOrDefaultAsync(x => x.Name == sportName);
            return s;
        }
    }
}
