using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels.ContestantViewModels
{
    public class AddContestantViewModel
    {
        // Add == true if event is selected
        public bool Add{ get; set; }

        // Event Data
        public int EventId { get; set; }
        public string EventName { get; set; }
        public List<SelectListItem> Heats { get; set; }

        // ContestantInHeat Data
        public string UserId { get; set; }
        public int HeatId { get; set; }
        public int Bib { get; set; }
        public string Team { get; set; }

        // Chip Data
        [Required]
        [Display(Name = "Chip Number (Zero means no chip)")]
        [Range(0, int.MaxValue, ErrorMessage = "ChipNumber must be a positive number")]
        public int ChipNumber { get; set; }
    }
}
