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
        private readonly TimingDbContext _tdb;

        public MarkerRepository(ApplicationDbContext db, TimingDbContext tdb)
        {
            _db = db;
            _tdb = tdb;
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

        public MarkerInHeat AddMarkerInHeat(MarkerInHeat m)
        {
            _db.MarkersInHeats.Add(m);
            _db.SaveChanges();
            return m;
        }

        public async Task<MarkerInHeat> AddMarkerInHeatAsync(MarkerInHeat m)
        {
            var Guntime = (from mih in _db.MarkersInHeats
                           where mih.HeatId == m.HeatId
                           join mar in _db.Markers on mih.MarkerId equals mar.Id
                           where mar.Type == Models.Entities.Type.Gun
                           select mar).SingleOrDefault();
            if(Guntime != null)
            {
                return null;
            }
            await _db.MarkersInHeats.AddAsync(m);
            await _db.SaveChangesAsync();
            return m;
        }

        public IEnumerable<MarkerInHeat> GetMarkersInHeats()
        {
            return _db.MarkersInHeats.ToList();
        }

        public MarkerInHeat GetMarkerInHeat(int markerId, int heatId)
        {
            return (from m in GetMarkersInHeats()
                    where m.MarkerId == markerId && m.HeatId == heatId
                    select m).SingleOrDefault();
        }

        public bool RemoveMarkerInHeat(MarkerInHeat m)
        {
            var result = false;
            if(_db.MarkersInHeats.Remove(m) != null)
            {
                result = true;
            }
            _db.SaveChanges();
            return result;
        }

        public async Task<bool> RemoveMarkerInHeatAsync(MarkerInHeat m)
        {
            var result = false;
            if (_db.MarkersInHeats.Remove(m) != null)
            {
                result = true;
            }
            await _db.SaveChangesAsync();
            return result;
        }

        public IEnumerable<Marker> GetMarkersFromTimingDb()
        {
            var result = (from m in _tdb.Markers
                          select new Marker
                          {
                              CompetitionInstanceId = m.CompetitionInstanceId,
                              Location = m.Location,
                              Time = m.MilliSecs,
                              Type = (m.Type == "Gunshot" ? Models.Entities.Type.Gun : Models.Entities.Type.Marker)
                          }).ToList();
            return result;
        }
    }
}
