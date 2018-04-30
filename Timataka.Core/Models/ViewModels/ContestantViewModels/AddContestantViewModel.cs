using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels.ContestantViewModels
{
    public class AddContestantViewModel
    {
        // Flag == true if event is selected
        public bool Flag { get; set; }

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
        public int ChipNumber { get; set; }
    }
}
