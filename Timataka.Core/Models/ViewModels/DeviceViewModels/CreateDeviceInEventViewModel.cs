using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Timataka.Core.Models.ViewModels.DeviceViewModels
{
    public class CreateDeviceInEventViewModel
    {
        [Required]
        public int DeviceId { get; set; }
        [Required]
        public int EventId { get; set; }
    }
}
