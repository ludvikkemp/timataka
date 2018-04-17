using System;
using System.Collections.Generic;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels.EventViewModels
{
    public class ContestantInEventViewModel
    {
        public int Bib { get; set; }
        public string ChipCode { get; set; }
        public ResultStatus Status { get; set; }
        public int HeatId { get; set; }
        public int HeatNumber { get; set; }
        public string Team { get; set; }
        public string Notes { get; set; }
        public DateTime Modified { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
        public IEnumerable<Heat> HeatsInEvent { get; set; }
    }
}