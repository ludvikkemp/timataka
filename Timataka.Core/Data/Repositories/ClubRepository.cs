using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext _db;
        private bool _disposed = false;

        public ClubRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Insert(Club c)
        {
            _db.Clubs.Add(c);
            _db.SaveChanges();
        }

        public async Task InsertAsync(Club c)
        {
            await _db.Clubs.AddAsync(c);
            await _db.SaveChangesAsync();
        }

        public IEnumerable<Club> Get()
        {
            return _db.Clubs.ToList();
        }

        public Club GetById(int id)
        {
            return _db.Clubs.SingleOrDefault(x => x.Id == id);
        }

        public Task<Club> GetByIdAsync(int id)
        {
            return _db.Clubs.SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Edit(Club c)
        {
            _db.Clubs.Update(c);
            _db.SaveChanges();
        }

        public async Task EditAsync(Club c)
        {
            _db.Clubs.Update(c);
            await _db.SaveChangesAsync();
        }

        public void Remove(Club c)
        {
            _db.Clubs.Update(c);
            _db.SaveChanges();
        }

        public async Task RemoveAsync(Club c)
        {
            _db.Clubs.Update(c);
            await _db.SaveChangesAsync();
        }

        public Task<Club> GetClubByNameAsync(string cName)
        {
            return _db.Clubs.SingleOrDefaultAsync(x => x.Name == cName);
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
