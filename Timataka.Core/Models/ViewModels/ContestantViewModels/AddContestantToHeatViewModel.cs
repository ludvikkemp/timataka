using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Timataka.Core.Models.ViewModels.ContestantViewModels
{
    public class AddContestantToHeatViewModel
    {
        public string UserId { get; set; }
        public string HeatId { get; set; }
        [Required]
        [Display(Name = "ChipNumber (0 equals no chip)")]
        [Range(0, int.MaxValue, ErrorMessage = "ChipNumber must be a positive number")]
        public int ChipNumber { get; set; }

        public int Bib { get; set; }
        public string Team { get; set; }
        public DateTime Modified { get; set; }
    }
}
