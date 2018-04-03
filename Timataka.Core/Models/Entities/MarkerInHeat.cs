using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public class MarkerInHeat
    {
        [Key]
        public int HeatId { get; set; }
        [ForeignKey(nameof(HeatId))]
        public virtual Heat Heat { get; set; }

        [Key]
        public int MarkerId { get; set; }
        [ForeignKey(nameof(MarkerId))]
        public virtual Marker Marker { get; set; }
    }
}
