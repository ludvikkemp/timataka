using System;
using System.Collections.Generic;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.Dto.CompetitionInstanceDTO
{
    public class EditContestantDto
    {
        public IEnumerable<Event> Events { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public int? NationId { get; set; }
        public string Phone { get; set; }
        public string CompetitionName { get; set; }
        public string CompetitionInstanceName { get; set; }
    }

}
