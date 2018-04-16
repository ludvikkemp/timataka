using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.ViewModels.ChipViewModels;

namespace Timataka.Core.Models.Dto.HeatDTO
{
    public class ChipsAssignedToHeatDto
    {
        public IEnumerable<ChipInHeatViewModel> ChipsInHeat { get; set; }

        // Breadcrumbs
        public string CompetitionName { get; set; }
        public string CompetitionInstanceName { get; set; }
        public string EventName { get; set; }
        public int HeatNumber { get; set; }
    }
}
