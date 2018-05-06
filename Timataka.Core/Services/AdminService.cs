using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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

        /// <summary>
        /// Function to update User.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>True if operation is successful, false otherwise</returns>
        public async Task<bool> UpdateUser(UserDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null) return false;

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

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
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
            return _repo.GetAdminUsers();
        }

        public IEnumerable<UserViewModel> GetNonAdminUsers()
        {
            return _repo.GetNonAdminUsers();
        }

        public async Task<UserViewModel> GetUserByIdAsync(string userId)
        {
            return await _repo.GetUserByIdAsync(userId);
        }
    }
}
