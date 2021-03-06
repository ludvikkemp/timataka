﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public bool Deleted { get; set; } = false;

        public int? CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public virtual Country RepresentingCountry { get; set; }

        public int? NationalityId { get; set; }
        [ForeignKey(nameof(NationalityId))]
        public virtual Country Nation { get; set; }
    }
}
