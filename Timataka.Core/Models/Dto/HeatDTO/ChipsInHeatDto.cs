using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.ChipViewModels;
using Timataka.Core.Models.ViewModels.HeatViewModels;

namespace Timataka.Core.Models.Dto.HeatDTO
{
    public class ChipsInHeatDto
    {
        public IEnumerable<ContestantsInHeatViewModel> UsersInHeat { get; set; }
        public IEnumerable<Chip> Chips { get; set; }
        public int HeatId { get; set; }
    }
}
