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
using Timataka.Core.Models.ViewModels.AdminViewModels;

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
                user.Email = model.Email;
                user.NationalityId = model.NationalityId;

                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public object GetRoles()
        {
            return _repo.GetRoles();
        }

        public string GetCountryNameById(int id)
        {
            return _repo.GetCountryNameById(id);
        }

        public string GetNationalityById(int id)
        {
            return _repo.GetNationalityById(id);
        }

        public IEnumerable<UserDto> GetUsersNotInHeatId(int heatId, int competitionInstanceId)
        {
            return _repo.GetUserNotInHeatId(heatId, competitionInstanceId);
        }

        public IEnumerable<UserViewModel> GetAdminUsers()
        {
            var admins = _repo.GetAdminUsers();
            return admins;
        }

        public IEnumerable<UserViewModel> GetNonAdminUsers()
        {
            var nonAdmins = _repo.GetNonAdminUsers();
            return nonAdmins;
        }

        public async Task<UserViewModel> GetUserByIdAsync(string userId)
        {
            UserViewModel user = await _repo.GetUserByIdAsync(userId);
            return user;
        }
    }
}
