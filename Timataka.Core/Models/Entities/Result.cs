using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public enum ResultStatus
    {
        [Display(Name = "Valid")]
        Valid = 0,
        [Display(Name = "Did Not Start")]
        DNS = 1,
        [Display(Name = "Did Not Finih")]
        DNF = 2,
        [Display(Name = "Disqualified")]
        DQ = 3
    }
    public class Result
    {
        [Key]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Key]
        public int HeatId { get; set; }
        [ForeignKey(nameof(HeatId))]
        public virtual Heat Heat { get; set; }

        public string Country { get; set; }
        public string Nationality { get; set; }
        public ResultStatus Status { get; set; }
        public string FinalTime { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public string Club { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
