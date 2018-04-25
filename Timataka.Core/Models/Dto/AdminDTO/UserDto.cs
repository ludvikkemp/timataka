using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Text;

namespace Timataka.Core.Models.Dto.AdminDTO
{
    public class UserRolesDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class UserDto
    {
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string Middlename { get; set; }

        [Required]
        public string LastName { get; set; }
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        public string Ssn { get; set; }
        public string Phone { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public bool Deleted { get; set; } = false;
        public int? CountryId { get; set; }
        public string Country { get; set; }
        [Display(Name = "Nationality")]
        public int? NationalityId { get; set; }
        public string Nationality { get; set; }
        public List<UserRolesDto> Roles { get; set; }
    }
}
