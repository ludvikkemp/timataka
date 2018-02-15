using System;
using Microsoft.AspNetCore.Identity;

namespace Timataka.Core.Models.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Ssn { get; set; }
        public string Phone { get; set; }
        public string Nationality { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

    }


}
