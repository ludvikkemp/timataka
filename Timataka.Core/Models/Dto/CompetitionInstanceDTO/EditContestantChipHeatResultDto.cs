using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.Dto.CompetitionInstanceDTO
{
    public class EditContestantChipHeatResultDto
    {
        public int Bib { get; set; }
        public int ChipNumber { get; set; }
        public string ChipCode { get; set; }
        public ResultStatus Status { get; set; }
        public int HeatNumber { get; set; }
        public int HeatId { get; set; }
        public string Notes { get; set; }
        public DateTime ResultModified { get; set; }
        public DateTime ContestantInHeatModified { get; set; }
        public string Team { get; set; }
    }
}
