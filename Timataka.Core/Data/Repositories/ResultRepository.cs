using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.HomeViewModels;

namespace Timataka.Core.Data.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private bool _disposed = false;
        private readonly ApplicationDbContext _db;

        public ResultRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Result Add(Result r)
        {
            _db.Results.Add(r);
            _db.SaveChanges();
            return r;
        }

        public async Task<Result> AddAsync(Result r)
        {
            throw new NotImplementedException();
        }

        public bool Edit(Result r)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditAsync(Result r)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Result> Get()
        {
            throw new NotImplementedException();
        }

        public Result GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Result r)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(Result r)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ResultViewModel> GetResultViewModelsForEvent(int eventId)
        {
            var results = (from r in _db.Results
                               join u in _db.Users on r.UserId equals u.Id
                               join h in _db.Heats on r.HeatId equals h.Id
                               where h.EventId == eventId
                               select new ResultViewModel
                               {
                                   Club = r.Club,
                                   Country = r.Contry
                               }).ToList();
            return results;
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
