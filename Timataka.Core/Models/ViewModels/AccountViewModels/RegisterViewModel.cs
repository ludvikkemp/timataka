using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Timataka.Core.Models.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Given Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name (Not Required)")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "If other please identify")]
        public string OtherGender { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number (Example: 354-5555555)")]
        public string Phone { get; set; }

        [StringLength(10, ErrorMessage = "SSN Must Contain 10 Characters", MinimumLength = 10)]
        [Display(Name = "Social Security Number")]
        public string Ssn { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Country of Residence")]
        public int CountryId { get; set; }

        [Required]
        [Display(Name = "Nationality")]
        public int NationId { get; set; }
    }
}
