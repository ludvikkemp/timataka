using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Timataka.Core.Models.Dto.AdminDTO;

namespace Timataka.Core.Data.Repositories
{
    public class AdminRepository : IAdminRepository
    {
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
            var countryName = (from c in _db.Countries where c.Id == id select c.Name).ToString();
            return countryName;
        }
    }
}
