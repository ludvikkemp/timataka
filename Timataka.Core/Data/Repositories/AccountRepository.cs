using System;
using System.Collections.Generic;
using System.Linq;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _db;
        private bool _disposed;

        public AccountRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Country> GetNations()
        {
            return _db.Countries.OrderBy(x => x.Name).ToList();
        }

        public Country GetCountryById(int id)
        {
            return _db.Countries.SingleOrDefault(x => x.Id == id);
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
