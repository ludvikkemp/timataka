using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Timataka.Core.Data;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Dto.AdminDTO;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repo;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminService(IAdminRepository repo, UserManager<ApplicationUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        public IEnumerable<UserDto> GetUsers()
        {
            return _repo.GetUsers();
        }

        public UserDto GetUserByUsername(string username)
        {
            return _repo.GetUserByUsername(username);
        }

        public async Task<bool> UpdateUser(UserDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.MiddleName = model.Middlename;
                user.LastName = model.LastName;
                user.CountryId = model.CountryId;
                user.DateOfBirth = model.DateOfBirth;
                user.Gender = model.Gender;
                user.Phone = model.Phone;
                user.Ssn = model.Ssn;

                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
