using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    public class CompetitionRepository : ICompetitionRepository
    {
        private bool _disposed = false;
        private readonly ApplicationDbContext _context;

        public CompetitionRepository(ApplicationDbContext context)
        {
            _context = context;
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

        public void Create(Competition c)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Competition c)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Competition> Get()
        {
            throw new NotImplementedException();
        }

        public Competition GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Competition> GetByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Competition c)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(Competition c)
        {
            throw new NotImplementedException();
        }

        public void Remove(Competition c)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Competition c)
        {
            throw new NotImplementedException();
        }

        public Task<Competition> GetCompetitionByNameAsync(string cName)
        {
            throw new NotImplementedException();
        }
    }
}
