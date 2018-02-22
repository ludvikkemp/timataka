using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    
    public class DiceplineRepository : IDiciplineRepository
    {
        private bool _disposed = false;
        private readonly ApplicationDbContext _context;

        public DiceplineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Dicipline> Get()
        {
            return _context.Diciplines.ToList();
        }

        public Dicipline GetById(int id)
        {
            return _context.Diciplines.SingleOrDefault(x => x.Id == id);
        }

        public Task<Dicipline> GetByIdAsync(int id)
        {
            return _context.Diciplines.SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Insert(Dicipline entity)
        {
            _context.Diciplines.Add(entity);
            _context.SaveChanges();
        }

        public async Task InsertAsync(Dicipline entity)
        {
            _context.Diciplines.Add(entity);
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
    }
}
