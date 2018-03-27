using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        private bool _disposed = false;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Insert(Category c)
        {
            _db.Categories.Add(c);
            _db.SaveChanges();
        }

        public async Task InsertAsync(Category c)
        {
            await _db.Categories.AddAsync(c);
            await _db.SaveChangesAsync();
        }

        public IEnumerable<Category> Get()
        {
            return _db.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return _db.Categories.SingleOrDefault(x => x.Id == id);
        }

        public Task<Category> GetByIdAsync(int id)
        {
            return _db.Categories.SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Edit(Category c)
        {
            _db.Categories.Update(c);
            _db.SaveChanges();
        }

        public async Task EditAsync(Category c)
        {
            _db.Categories.Update(c);
            await _db.SaveChangesAsync();
        }

        public void Remove(Club c)
        {
            c.Deleted = true;
            _db.Clubs.Update(c);
            _db.SaveChanges();
        }

        public async Task RemoveAsync(Club c)
        {
            c.Deleted = true;
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
