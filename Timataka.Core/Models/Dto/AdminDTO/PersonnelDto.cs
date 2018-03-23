using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;

namespace Timataka.Core.Models.Dto.AdminDTO
{
    public class PersonnelDto
    {
        public Competition Competition { get; set; }
        public IEnumerable<UserDto> Users { get; set; }
        public IEnumerable<ManagesCompetitionViewModel> AssignedRoles { get; set; }
        public Role Roles { get; set; }
    }
}
