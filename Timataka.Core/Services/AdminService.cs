using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Dto.AdminDTO;

namespace Timataka.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repo;

        public AdminService(IAdminRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<UserDto> GetUsers()
        {
            return _repo.GetUsers();
        }

        public UserDto GetUserByUsername(string username)
        {
            return _repo.GetUserByUsername(username);
        }

    }
}
