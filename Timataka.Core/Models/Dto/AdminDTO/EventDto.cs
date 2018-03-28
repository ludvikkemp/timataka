using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.HeatViewModels;

namespace Timataka.Core.Models.Dto.AdminDTO
{
    public class EventDto
    {
        public Event Event { get; set; }
        public IEnumerable<HeatViewModel> Heats { get; set; }
    }
}
