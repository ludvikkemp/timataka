using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels.DeviceViewModels
{
    public class DeviceInEventViewModel
    {
        public DevicesInEvent DevicesInEvent { get; set; }

        public Event Event { get; set; }

        public Device Device { get; set; }
    }
}
