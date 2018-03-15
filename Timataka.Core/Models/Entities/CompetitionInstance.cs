using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public enum Status
    {
        Pending = 0,
        OpenForRegistration = 1,
        Ongoing = 2,
        Finished = 3,
        Closed = 4
    }
    public class CompetitionInstance
    {
        public int Id { get; set; }
        public int CompetitionId { get; set; }
        [ForeignKey(nameof(CompetitionId))]
        public virtual Discipline _CompetitonId { get; set; }
        //Start of first event
        public DateTime DateFrom { get; set; }
        //End of last event
        public DateTime DateTo { get; set; }
        public String Location { get; set; }
        public int? CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }
        public string CompetitionName { get; set; }
        public Status Status { get; set; }
        public Boolean Deleted { get; set; }

    }
}
