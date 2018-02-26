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
                    Name = u.FirstName + " " + u.LastName,
                    Email = u.Email,
                    Username = u.UserName,
                    Ssn = u.Ssn,
                    Phone = u.Phone,
                    DateOfBirth = u.DateOfBirth,
                    Gender = u.Gender,
                    CountryId = u.CountryId,
                    NationalityId = u.NationalityId,
                    Deleted = u.Deleted,
                    Roles = (from ur in _db.UserRoles
                             join r in _db.Roles 
                             on ur.RoleId equals r.Id
                             where u.Id == ur.UserId
                             select new RolesDto
                             {
                                 Id = ur.RoleId,
                                 Name = r.Name
                             }).ToList()
                }).ToList();
            return users;
        }
    }
}
