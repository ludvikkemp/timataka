using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Timataka.Core.Models.ViewModels.AccountViewModels
{
    public class IcelandicRegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Veffang")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0}ið verður að vera a.m.k. {2} stafir og mest {1} stafir.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lykilorð")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Staðfesta Lykilorð")]
        [Compare("Password", ErrorMessage = "Lykilorðin eru ekki eins.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Fornafn")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name (Not Required)")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Eftirnafn")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Kyn")]
        public string Gender { get; set; }

        [Display(Name = "Ef annað skrá hér:")]
        public string OtherGender { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Símanúmer (Dæmi: 354-5555555)")]
        public string Phone { get; set; }

        [StringLength(10, ErrorMessage = "Kennitala verður að hafa 10 tölustafi", MinimumLength = 10)]
        [Display(Name = "Kennitala")]
        public string Ssn { get; set; }

        [Required]
        [Display(Name = "Fæðingardagur")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Búseta")]
        public int CountryId { get; set; }

        [Required]
        [Display(Name = "Þjóðerni")]
        public int NationId { get; set; }
    }
}
