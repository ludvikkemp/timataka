using System;
using System.Collections.Generic;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels.EventViewModels
{
    public class ContestantInEventViewModel
    {
        public int Bib { get; set; }
        public IEnumerable<ChipInHeat> Chips { get; set; }
        public ResultStatus Status { get; set; }
        public int HeatId { get; set; }
        public int HeatNumber { get; set; }
        public string Team { get; set; }
        public string Notes { get; set; }
        public DateTime Modified { get; set; }
    }
}