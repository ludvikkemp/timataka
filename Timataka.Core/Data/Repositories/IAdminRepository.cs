using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Dto.AdminDTO;
using Timataka.Core.Models.ViewModels.AdminViewModels;

namespace Timataka.Core.Data.Repositories
{
    public interface IAdminRepository
    {
        IEnumerable<UserDto> GetUsers();
        UserDto GetUserByUsername(string username);
        UserViewModel GetUserById(string userId);
        IEnumerable<UserRolesDto> GetRoles();
        string GetCountryNameById(int id);
        IEnumerable<UserViewModel> GetAdminUsers();
        IEnumerable<UserViewModel> GetNonAdminUsers();
        string GetNationalityById(int id);
    }
}
