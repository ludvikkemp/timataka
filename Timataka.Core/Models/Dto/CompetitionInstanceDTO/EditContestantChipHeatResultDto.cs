using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.Dto.CompetitionInstanceDTO
{
    public class EditContestantChipHeatResultDto
    {
        public int Bib { get; set; }
        public string ChipCode { get; set; }
        public ResultStatus Status { get; set; }
        public string HeatNumber { get; set; }
        public int HeatId { get; set; }
        public string Notes { get; set; }
        public DateTime Modified { get; set; }
        public string Team { get; set; }
    }
}
