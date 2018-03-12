using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Dto.AdminDTO;

namespace Timataka.Core.Services
{
    public interface IAdminService
    {
        IEnumerable<UserDto> GetUsers();
        UserDto GetUserByUsername(string username);
        Task<bool> UpdateUser(UserDto model);
    }
}
