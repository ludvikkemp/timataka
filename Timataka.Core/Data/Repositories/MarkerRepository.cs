using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    public class MarkerRepository : IMarkerRepository
    {
        private bool _disposed = false;
        private readonly ApplicationDbContext _db;

        public MarkerRepository(ApplicationDbContext db)
        {
            _db = db;
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

        public Marker Add(Marker m)
        {
            _db.Markers.Add(m);
            _db.SaveChanges();
            return m;
        }

        public async Task<Marker> AddAsync(Marker m)
        {
            await _db.Markers.AddAsync(m);
            await _db.SaveChangesAsync();
            return m;
        }

        public IEnumerable<Marker> Get()
        {
            return _db.Markers.ToList();
        }

        public Marker GetById(int id)
        {
            var result = (from x in _db.Markers
                          where x.Id == id
                          select x).SingleOrDefault();
            return result;
        }

        public async Task<Marker> GetByIdAsync(int id)
        {
            var result = await (from x in _db.Markers
                                where x.Id == id
                                select x).SingleOrDefaultAsync();
            return result;
        }

        public Boolean Edit(Marker m)
        {
            var result = false;
            if(_db.Markers.Update(m) != null)
            {
                result = true;
            }
            _db.SaveChanges();
            return result;
        }

        public async Task<Boolean> EditAsync(Marker m)
        {
            var result = false;
            if (_db.Markers.Update(m) != null)
            {
                result = true;
            }
            await _db.SaveChangesAsync();
            return result;
        }

        public Boolean Remove(Marker m)
        {
            var result = false;
            if(_db.Markers.Remove(m) != null)
            {
                result = true;
            }
            _db.SaveChanges();
            return result;
        }

        public async Task<Boolean> RemoveAsync(Marker m)
        {
            var result = false;
            if(_db.Markers.Remove(m) != null)
            {
                result = true;
            }
            await _db.SaveChangesAsync();
            return result;
        }
    }
}
