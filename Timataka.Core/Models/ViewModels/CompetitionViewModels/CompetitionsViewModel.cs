using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels.CompetitionViewModels
{
    public class CompetitionsViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Url]
        public string WebPage { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string Sponsor { get; set; }
    }

    public class CompetitionsInstanceViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Competition")]
        public int CompetitionId { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }
        public string Location { get; set; }
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public Status Status { get; set; }
        public bool Deleted { get; set; }
    }
}
