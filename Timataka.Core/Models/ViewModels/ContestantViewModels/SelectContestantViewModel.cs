using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Dto.AdminDTO;

namespace Timataka.Core.Models.ViewModels.ContestantViewModels
{
    public class SelectContestantViewModel
    {
        public string CompetitionName { get; set; }
        public string CompetitionInstanceName { get; set; }
        public string EventName { get; set; }
        public IEnumerable<UserDto> Users { get; set; }
    }
}
