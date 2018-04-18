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
        public string ChipCode { get; set; }
        public ResultStatus Status { get; set; }
        public string HeatNumber { get; set; }
        public int HeatId { get; set; }
        public string Notes { get; set; }
        public DateTime Modified { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public int? NationId { get; set; }
        public string Phone { get; set; }
        public IEnumerable<Heat> HeatsInEvent { get; set; }
    }
}
