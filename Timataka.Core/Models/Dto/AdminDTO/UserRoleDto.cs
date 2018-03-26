using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.ViewModels.AdminViewModels;

namespace Timataka.Core.Models.Dto.AdminDTO
{
    public class UserRoleDto
    {
        public IEnumerable<UserViewModel> Admins { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}
