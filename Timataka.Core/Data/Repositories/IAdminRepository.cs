using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Dto.AdminDTO;
using Timataka.Core.Models.ViewModels.AdminViewModels;

namespace Timataka.Core.Data.Repositories
{
    public interface IAdminRepository : IDisposable
    {
        IEnumerable<UserDto> GetUsers();
        UserDto GetUserByUsername(string username);
        Task<UserViewModel> GetUserByIdAsync(string userId);
        IEnumerable<UserRolesDto> GetRoles();
        string GetCountryNameById(int id);
        IEnumerable<UserViewModel> GetAdminUsers();
        IEnumerable<UserViewModel> GetNonAdminUsers();
        string GetNationalityById(int id);
    }
}
