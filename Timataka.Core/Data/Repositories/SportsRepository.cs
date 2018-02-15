﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    
    public class SportsRepository : ISportsRepository
    {
        private bool _disposed = false;
        private readonly ApplicationDbContext _context;

        public SportsRepository(ApplicationDbContext context)
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
            _context.Sports.Add(entity);
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
