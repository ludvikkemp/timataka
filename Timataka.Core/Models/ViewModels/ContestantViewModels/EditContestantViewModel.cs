using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels.ContestantViewModels
{
    public class EditContestantViewModel
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string Team { get; set; }
        public int Bib { get; set; }
        public ResultStatus ResultStatus { get; set; }
        public string Notes { get; set; }
        public DateTime Modified { get; set; }
        public int HeatId { get; set; }
        public string HeatNumber { get; set; }
        public string ChipCode { get; set; }
        public IEnumerable<Heat> HeatsInEvent { get; set; }
    }
}
