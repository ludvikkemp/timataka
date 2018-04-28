using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Timataka.Core.Models.Dto.AdminDTO;
using Timataka.Core.Models.ViewModels.AdminViewModels;

namespace Timataka.Core.Data.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private bool _disposed = false;
        private readonly ApplicationDbContext _db;

        public AdminRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<UserDto> GetUsers()
        {
            var users = (from u in _db.Users
                select new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    Middlename = u.MiddleName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Username = u.UserName,
                    Ssn = u.Ssn,
                    Phone = u.Phone,
                    DateOfBirth = u.DateOfBirth,
                    Gender = u.Gender,
                    CountryId = u.CountryId,
                    Country = (from c in _db.Countries
                               where c.Id == u.CountryId
                               select c.Name).FirstOrDefault(),
                    NationalityId = u.NationalityId,
                    Deleted = u.Deleted,
                    Roles = (from ur in _db.UserRoles
                             join r in _db.Roles 
                             on ur.RoleId equals r.Id
                             where u.Id == ur.UserId
                             select new UserRolesDto
                             {
                                 Id = ur.RoleId,
                                 Name = r.Name
                             }).ToList()
                }).ToList();
            return users;
        }

        public UserDto GetUserByUsername(string username)
        {
            var user = (from u in _db.Users
                        where u.UserName == username
                        select new UserDto
                        {
                            FirstName = u.FirstName,
                            Middlename = u.MiddleName,
                            LastName = u.LastName,
                            Email = u.Email,
                            Username = u.UserName,
                            Ssn = u.Ssn,
                            Phone = u.Phone,
                            DateOfBirth = u.DateOfBirth,
                            Gender = u.Gender,
                            CountryId = u.CountryId,
                            Country = (from c in _db.Countries
                                where c.Id == u.CountryId
                                select c.Name).FirstOrDefault(),
                            NationalityId = u.NationalityId,
                            Nationality = (from c in _db.Countries
                                where c.Id == u.NationalityId
                                select c.Nationality).FirstOrDefault(),
                            Deleted = u.Deleted,
                            Roles = (from ur in _db.UserRoles
                                join r in _db.Roles
                                    on ur.RoleId equals r.Id
                                where u.Id == ur.UserId
                                select new UserRolesDto
                                {
                                    Id = ur.RoleId,
                                    Name = r.Name
                                }).ToList()
                        }).SingleOrDefault();
            return user;
        }

        public IEnumerable<UserRolesDto> GetRoles()
        {
            var roles = (from r in _db.Roles
                select new UserRolesDto
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToList();
            return roles;
        }

        public string GetCountryNameById(int id)
        {
            var countryName = (from c in _db.Countries where c.Id == id select c.Name).FirstOrDefault();
            return countryName;
        }

        public IEnumerable<UserViewModel> GetAdminUsers()
        {
            var admins = (from a in _db.Users
                join r in _db.UserRoles on a.Id equals r.UserId
                join ro in _db.Roles on r.RoleId equals ro.Id
                where ro.Name == "Admin"
                select new UserViewModel
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Username = a.UserName,
                    Role = "Admin"

                }).ToList();
            return admins;
        }

        public IEnumerable<UserViewModel> GetNonAdminUsers()
        {
            var users = GetUsers();
            return (from user in users
                where user.Roles.Count == 1
                select new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Role = "User"
                }).ToList();
        }

        public async Task<UserViewModel> GetUserByIdAsync(string userId)
        {
           var user = await (from u in _db.Users
                            where u.Id == userId
                            select new UserViewModel
                            {
                                Id = u.Id,
                                DateOfBirth = u.DateOfBirth,
                                Deleted = u.Deleted,
                                FirstName = u.FirstName,
                                Gender = u.Gender,
                                LastName = u.LastName,
                                Phone = u.Phone,
                                Username = u.UserName,
                                Ssn = u.Ssn,
                                Country = (int)u.CountryId,
                                Nationality = (int)u.NationalityId
                            }).SingleOrDefaultAsync();
            return user;
        }

        public string GetNationalityById(int id)
        {
            var nationality = (from c in _db.Countries where c.Id == id select c.Nationality).FirstOrDefault();
            return nationality;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }

            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
