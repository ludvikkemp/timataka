using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.DeviceViewModels;
using Timataka.Core.Models.ViewModels.EventViewModels;

namespace Timataka.Core.Services
{
    public interface IDeviceService
    {
        // *** ADD & ASSIGN *** //
        Task<Device> AddAsync(DeviceViewModel d);
        Task<DevicesInEvent> AddDeviceInEventAsync(int dId, int eId);
        Boolean DeviceAssigned(DevicesInEvent d);

        // *** EDIT & REMOVE *** //
        Task<Boolean> EditAsync(Device d);
        Task<Boolean> RemoveAsync(int id);
        Task<Boolean> RemoveDeviceInEventAsync(DevicesInEvent d);

        // *** GET *** //
        IEnumerable<Device> GetDevices();
        Task<Device> GetDeviceByIdAsync(int id);
        IEnumerable<Device> GetDevicesByType(Boolean active);
        Device GetDeviceByNameAsync(String name);
        IEnumerable<DeviceInEventViewModel> GetDevicesInEvent(int id);
        IEnumerable<Device> GetUnassignedDevicesForEvent(int eventId);
        IEnumerable<DeviceInEventViewModel> GetDevicesInCompetitionInstance(int id);
        IEnumerable<EventListForDevicesViewModel> GetEventsForADevice(int id);

        // *** EXISTS *** //
        Boolean Exists(int id);
        Boolean NameExists(String name);

        
    }
}
