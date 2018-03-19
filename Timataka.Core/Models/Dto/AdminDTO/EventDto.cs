using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.Dto.AdminDTO
{
    public class EventDto
    {
        public Event Event { get; set; }
        public IEnumerable<Heat> Heats { get; set; }
    }
}
