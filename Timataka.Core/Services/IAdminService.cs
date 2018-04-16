using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Dto.AdminDTO;
using Timataka.Core.Models.ViewModels.AdminViewModels;

namespace Timataka.Core.Services
{
    public interface IAdminService
    {
        IEnumerable<UserDto> GetUsers();
        UserDto GetUserByUsername(string username);
        Task<bool> UpdateUser(UserDto model);
        object GetRoles();
        string GetCountryNameById(int id);
        IEnumerable<UserViewModel> GetAdminUsers();
        IEnumerable<UserViewModel> GetNonAdminUsers();
        UserViewModel GetUserById(string userId);
        string GetNationalityById(int nationality);
    }
}
