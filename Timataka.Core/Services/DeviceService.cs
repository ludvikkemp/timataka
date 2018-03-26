using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.DeviceViewModels;

namespace Timataka.Core.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _repo;
        private readonly IEventService _eventService;

        public DeviceService(IDeviceRepository repo, IEventService eventService)
        {
            _repo = repo;
            _eventService = eventService;
        }

        /// <summary>
        /// Function to add a Device.
        /// </summary>
        /// <param name="d">Device view model</param>
        /// <returns>The created device</returns>
        public async Task<Device> AddAsync(DeviceViewModel d)
        {            
            if(NameExists(d.Name))
            {
                throw new Exception("Device with the name \"" + d.Name + "\" already exists. Please choose a different name.");
            }
            return await _repo.AddAsync(new Device { Name = d.Name, Active = d.Active });
        }

        /// <summary>
        /// Function to assign device to an event.
        /// </summary>
        /// <param name="dId"></param>
        /// <param name="eId"></param>
        /// <returns>Returns instance of the assignment.</returns>
        public async Task<DevicesInEvent> AddDeviceInEventAsync(int dId, int eId)
        {
            var d = new DevicesInEvent { DeviceId = dId, EventId = eId };
            if (DeviceAssigned(d) == false)
            {
                await _repo.AddDeviceToEventAsync(d);
                return d;
            }
            else
            {
                var n = await GetDeviceByIdAsync(dId);
                throw new Exception(n.Name + " is already assigned to this event");
            }
        }

        /// <summary>
        /// Function to check if device is assigned to an event.
        /// </summary>
        /// <param name="dId"></param>
        /// <param name="eId"></param>
        /// <returns>True if device is assigned, false otherwise.</returns>
        public Boolean DeviceAssigned(DevicesInEvent d)
        {
            var r = (from x in _repo.GetDevicesInEvents()
                     where x.DeviceId == d.DeviceId & x.EventId == d.EventId
                     select x).Any();
            return r;
        }

        /// <summary>
        /// Function to edit devices
        /// </summary>
        /// <param name="d">Device</param>
        /// <returns>True if edit was successfull, false otherwise</returns>
        public async Task<Boolean> EditAsync(Device d)
        {
            Device oldDevice = await GetDeviceByIdAsync(d.Id);
            //Name changes, need to check for other devices with that new name
            if(oldDevice.Name != d.Name)
            {
                if(NameExists(d.Name))
                {
                    throw new Exception("Device with the name \"" + d.Name + "\" already exists.");
                }
            }
            return await _repo.EditAsync(d);
        }

        /// <summary>
        /// Function to check if device exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if device exists</returns>
        public Boolean Exists(int id)
        {
            Boolean r = (from x in _repo.Get()
                        where x.Id == id
                        select x).Any();
            return r;
        }

        /// <summary>
        /// Function to get device by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Device with the given id if it exists</returns>
        public async Task<Device> GetDeviceByIdAsync(int id)
        {
            if(Exists(id))
            {
                return await _repo.GetByIdAsync(id);
            }
            else
            {
                throw new Exception("Device does not exists");
            }
        }

        /// <summary>
        /// Function to get device by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Device with the given name if it exists</returns>
        public Device GetDeviceByNameAsync(string name)
        {
            if(NameExists(name))
            {
                return (from x in GetDevices()
                        where x.Name == name
                        select x).SingleOrDefault(); ;
            }
            else
            {
                throw new Exception("Device does not exist");
            }
        }

        /// <summary>
        /// Function to get list of all devices
        /// </summary>
        /// <returns>List of all devices</returns>
        public IEnumerable<Device> GetDevices()
        {
            return _repo.Get();
        }

        /// <summary>
        /// Function to get devices by type.
        /// </summary>
        /// <param name="active">True for active and flase for passive</param>
        /// <returns>List of devices of the type specified</returns>
        public IEnumerable<Device> GetDevicesByType(bool active)
        {
            var result = (from x in GetDevices()
                          where x.Active == active
                          select x);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<DevicesInEvent> GetDevicesInEvent(int id)
        {
            var result = (from x in _repo.GetDevicesInEvents()
                          where x.EventId == id
                          select x);
            return result;
        }

        /// <summary>
        /// Function to get list of events for a device.
        /// </summary>
        /// <param name="id">ID of the device.</param>
        /// <returns>List of events</returns>
        public IEnumerable<Event> GetEventsForADevice(int id)
        {
            var result = (from x in _repo.GetDevicesInEvents()
                          where x.DeviceId == id
                          select _eventService.GetEventByIdAsync(id).Result);
            return result;
        }

        /// <summary>
        /// Function to check if a device with given name exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Boolean NameExists(string name)
        {
            Boolean d = (from x in GetDevices()
                     where x.Name == name
                     select x).Any();
            return d;
        }

        /// <summary>
        /// Function to remove devices
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if success, false otherwise</returns>
        public async Task<bool> RemoveAsync(int id)
        {
            return await _repo.RemoveAsync(await GetDeviceByIdAsync(id));
        }

        /// <summary>
        /// Function to unassign device from event.
        /// </summary>
        /// <param name="d"></param>
        /// <returns>True if success, false otherwise.</returns>
        public async Task<Boolean> RemoveDeviceInEventAsync(DevicesInEvent d)
        {
            return await _repo.RemoveDeviceInEventAsync(d);
        }
    }
}
