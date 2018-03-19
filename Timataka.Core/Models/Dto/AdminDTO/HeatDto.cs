using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.Dto.AdminDTO
{
    public class HeatDto
    {
        public Heat Heat { get; set; }
        public IEnumerable<ContestantInHeat> Contestants { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
