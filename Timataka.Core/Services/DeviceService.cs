using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.DeviceViewModels;
using Timataka.Core.Models.ViewModels.EventViewModels;

namespace Timataka.Core.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _repo;
        private readonly IEventService _eventService;
        private readonly ICompetitionService _competitionService;

        public DeviceService(IDeviceRepository repo, 
                                IEventService eventService, 
                                ICompetitionService competitionService)
        {
            _repo = repo;
            _eventService = eventService;
            _competitionService = competitionService;
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
            oldDevice.Name = d.Name;
            oldDevice.Active = d.Active;
            return await _repo.EditAsync(oldDevice);
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
        /// Function that returns all devices for given event.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<DeviceInEventViewModel> GetDevicesInEvent(int id)
        {
            var e = _eventService.GetEventById(id);
            var result = (from x in _repo.GetDevicesInEvents()
                          join d in _repo.Get() on x.DeviceId equals d.Id
                          where x.EventId == id
                          select new DeviceInEventViewModel
                          {
                              DeviceName = d.Name,
                              EventName = e.Name,
                              Active = x.Device.Active,
                              DateFrom = e.DateFrom,
                              DateTo = e.DateTo,
                              DevicesInEvent = x
                          });
            return result;
        }

        /// <summary>
        /// Function to get all devices for a competition instance
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<DeviceInEventViewModel> GetDevicesInCompetitionInstance(int id)
        {
            var result = from e in _eventService.GetEventsByCompetitionInstanceId(id)
                         join devicesInEvents in _repo.GetDevicesInEvents() on e.Id equals devicesInEvents.EventId
                         join d in _repo.Get() on devicesInEvents.DeviceId equals d.Id
                         select new DeviceInEventViewModel
                         {
                             DeviceName = d.Name,
                             EventName = e.Name,
                             Active = d.Active,
                             DateFrom = e.DateFrom,
                             DateTo = e.DateTo,
                             DevicesInEvent = devicesInEvents
                         };
            return result;
        }

        /// <summary>
        /// Function to get list of events for a device.
        /// </summary>
        /// <param name="id">ID of the device.</param>
        /// <returns>List of events</returns>
        public IEnumerable<EventListForDevicesViewModel> GetEventsForADevice(int id)
        {
            var result = (from x in _repo.GetDevicesInEvents()
                         where x.DeviceId == id
                         select new EventListForDevicesViewModel
                         {
                             Event = _eventService.GetEventByIdAsync(x.EventId).Result,
                             CompetitionInstanceId = _eventService.GetEventByIdAsync(x.EventId).Result.CompetitionInstanceId,
                             CompetitionId = _competitionService.GetCompetitionInstanceByIdAsync(_eventService.
                                                GetEventByIdAsync(x.EventId).Result.CompetitionInstanceId).Result.CompetitionId
                         }).ToList();
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

        /// <summary>
        /// Get list of devices that have not been assigned to an event.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns>List of devices</returns>
        public IEnumerable<Device> GetUnassignedDevicesForEvent(int eventId)
        {
            return (from d in GetDevices()
                    where DeviceAssigned(new DevicesInEvent { DeviceId = d.Id, EventId = eventId }) == false
                    select d).ToList();
        }
    }
}
