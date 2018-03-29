using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public class ChipInHeat
    {
        [Key]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set;}

        public int HeatId { get; set; }
        [ForeignKey(nameof(HeatId))]
        public virtual Heat Heat { get; set; }

        [Key]
        public string ChipCode { get; set; }
        [ForeignKey(nameof(ChipCode))]
        public virtual Chip Chip { get; set; }

        public Boolean Valid { get; set; }
    }
}
