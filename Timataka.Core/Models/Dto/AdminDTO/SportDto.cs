using System.Collections.Generic;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.DisciplineViewModels;

namespace Timataka.Core.Models.Dto.AdminDTO
{
    public class SportDto
    {
        public Sport Sport { get; set; }
        public IEnumerable<DisciplineViewModel> Disciplines { get; set; }
    }
}
