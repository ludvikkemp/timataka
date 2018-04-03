using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels.EventViewModels
{
    public class EventListForDevicesViewModel
    {
        public Event Event { get; set; }
        public int CompetitionInstanceId { get; set; }
        public int CompetitionId { get; set; }
    }
}
