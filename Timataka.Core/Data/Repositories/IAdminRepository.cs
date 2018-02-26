using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Dto.AdminDTO;

namespace Timataka.Core.Data.Repositories
{
    public interface IAdminRepository
    {
        IEnumerable<UserDto> GetUsers();
        UserDto GetUserByUsername(string username);
    }
}
