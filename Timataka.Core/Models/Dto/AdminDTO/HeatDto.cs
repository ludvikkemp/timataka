using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.HeatViewModels;

namespace Timataka.Core.Models.Dto.AdminDTO
{
    public class HeatDto
    {
        public Heat Heat { get; set; }
        public IEnumerable<ContestantsInHeatViewModel> Contestants { get; set; }
    }
}
