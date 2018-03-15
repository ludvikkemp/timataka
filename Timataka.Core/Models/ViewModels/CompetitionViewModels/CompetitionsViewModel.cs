using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Timataka.Core.Models.ViewModels.CompetitionViewModels
{
    public class CompetitionsViewModel
    {
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
}
