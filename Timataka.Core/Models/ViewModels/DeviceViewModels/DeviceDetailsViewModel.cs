using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.EventViewModels;

namespace Timataka.Core.Models.ViewModels.DeviceViewModels
{
    public class DeviceDetailsViewModel
    {
        public Device Device { get; set; }
        public IEnumerable<EventListForDevicesViewModel> Events { get; set; }
    }
}
