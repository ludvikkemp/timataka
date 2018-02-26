using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;

namespace Timataka.Core.Models.Dto.AdminDTO
{
    public class RolesDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Ssn { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public bool Deleted { get; set; } = false;
        public int? CountryId { get; set; }
        public int? NationalityId { get; set; }
        public List<RolesDto> Roles { get; set; }
    }
}
