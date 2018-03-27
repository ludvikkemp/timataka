using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels.DeviceViewModels
{
    public class DeviceDetailsViewModel
    {
        public Device Device { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}
