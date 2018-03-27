using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private bool _disposed = false;
        private readonly ApplicationDbContext _db;

        public DeviceRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Device Add(Device d)
        {
            _db.Devices.Add(d);
            _db.SaveChanges();
            return d;
          
        }

        public async Task<Device> AddAsync(Device d)
        {
            await _db.Devices.AddAsync(d);
            await _db.SaveChangesAsync();
            return d;
        }

        public Boolean Edit(Device d)
        {
            var result = false;
            if(_db.Devices.Update(d) != null)
            {
                result = true;
            }
            _db.SaveChanges();
            return result;
        }

        public async Task<Boolean> EditAsync(Device d)
        {
            var result = false;
            if (_db.Devices.Update(d) != null)
            {
                result = true;
            }
            await _db.SaveChangesAsync();
            return result;
        }

        public IEnumerable<Device> Get()
        {
            return _db.Devices.ToList();
        }

        public Device GetById(int id)
        {
            var result = (from x in _db.Devices
                          where x.Id == id
                          select x).SingleOrDefault();
            return result;
        }

        public async Task<Device> GetByIdAsync(int id)
        {
            var result = await (from x in _db.Devices
                                where x.Id == id
                                select x).SingleOrDefaultAsync();
            return result;
        }

        public Boolean Remove(Device d)
        {
            Boolean result = false;
            if(_db.Devices.Remove(d) != null)
            {
                result = true;
            }
            _db.SaveChanges();
            return result;
        }

        public async Task<Boolean> RemoveAsync(Device d)
        {
            Boolean result = false;
            if (_db.Devices.Remove(d) != null)
            {
                result = true;
            }
            await _db.SaveChangesAsync();
            return result;
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

        public bool AddDeviceToEvent(DevicesInEvent d)
        {
            var result = false;
            if(_db.DevicesInEvents.Add(d) != null)
            {
                result = true;
            }
            _db.SaveChanges();
            return result;
        }

        public async Task<bool> AddDeviceToEventAsync(DevicesInEvent d)
        {
            var result = false;
            if (await _db.DevicesInEvents.AddAsync(d) != null)
            {
                result = true;
            }
            await _db.SaveChangesAsync();
            return result;
        }

        public IEnumerable<DevicesInEvent> GetDevicesInEvents()
        {
            return _db.DevicesInEvents.ToList();
        }

        public bool RemoveDeviceInEvent(DevicesInEvent d)
        {
            var result = false;
            if (_db.DevicesInEvents.Remove(d) != null)
            {
                result = true;
            }
            _db.SaveChanges();
            return result;
        }

        public async Task<bool> RemoveDeviceInEventAsync(DevicesInEvent d)
        {
            var result = false;
            if (_db.DevicesInEvents.Remove(d) != null)
            {
                result = true;
            }
            await _db.SaveChangesAsync();
            return result;
        }
    }
}