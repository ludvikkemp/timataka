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
        Task<Device> AddAsync(DeviceViewModel d);
        Task<Device> GetDeviceByIdAsync(int id);
        Task<Boolean> EditAsync(Device d);
        Task<Boolean> RemoveAsync(int id);
        IEnumerable<Device> GetDevices();
        IEnumerable<Device> GetDevicesByType(Boolean active);
        Device GetDeviceByNameAsync(String name);
        Boolean Exists(int id);
        Boolean NameExists(String name);

        Task<DevicesInEvent> AddDeviceInEventAsync(int dId, int eId);
        IEnumerable<DeviceInEventViewModel> GetDevicesInEvent(int id);
        IEnumerable<DeviceInEventViewModel> GetDevicesInCompetitionInstance(int id);
        IEnumerable<EventListForDevicesViewModel> GetEventsForADevice(int id);
        Task<Boolean> RemoveDeviceInEventAsync(DevicesInEvent d);
        Boolean DeviceAssigned(DevicesInEvent d);
    }
}
