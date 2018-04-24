using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.Dto.CompetitionInstanceDTO
{
    public class EditContestantInEventDto
    {
        public string CompetitionName { get; set; }
        public string CompetitionInstanceName { get; set; }
        public string EventName { get; set; }
        [Required]
        public int Bib { get; set; }
        public int ChipNumber { get; set; }
        public string OldChipCode { get; set; }
        public ResultStatus Status { get; set; }
        public int HeatNumber { get; set; }
        [Display(Name = "Heat Number")]
        public int HeatId { get; set; }
        public int OldHeatId { get; set; }
        public string Notes { get; set; }
        public DateTime ResultModified { get; set; }
        public DateTime ContestantInHeatModified { get; set; }
        public string Team { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public int? NationId { get; set; }
        public string Phone { get; set; }
        public IEnumerable<Heat> HeatsInEvent { get; set; }
    }
}
