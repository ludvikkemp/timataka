using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Dto.AdminDTO;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels.HeatViewModels
{
    public class SelectContestantViewModel2
    {
        public string CompetitionName { get; set; }
        public string CompetitionInstanceName { get; set; }
        public string EventName { get; set; }
        public int HeatNumber { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
