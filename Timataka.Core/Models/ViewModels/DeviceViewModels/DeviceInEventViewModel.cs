using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels.DeviceViewModels
{
    public class DeviceInEventViewModel
    {
        public DevicesInEvent DevicesInEvent { get; set; }

        public string EventName { get; set; }

        public string DeviceName { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public Boolean Active { get; set; }
    }
}
