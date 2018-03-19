using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public class ContestantInHeat
    {
        [Key]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Key]
        public int HeatId { get; set; }
        [ForeignKey(nameof(HeatId))]
        public virtual Heat Heat { get; set; }

        public int Bib { get; set; }
        public string Team { get; set; }
        public DateTime Modified { get; set; }
    }
}
