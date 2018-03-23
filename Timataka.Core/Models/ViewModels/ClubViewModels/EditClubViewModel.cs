using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Timataka.Core.Models.ViewModels.ClubViewModels
{
    public class EditClubViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string NameAbbreviation { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        [Url]
        public string Webpage { get; set; }
    }
}
