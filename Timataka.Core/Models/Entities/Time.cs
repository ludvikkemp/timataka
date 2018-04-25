using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public enum TimeType    
    {
        Start = 0,
        Split = 1,
        Finish = 2
    }
    public class Time
    {
        [Key]
        public string ChipCode { get; set; }
        [ForeignKey(nameof(ChipCode))]
        public virtual Chip Chip{ get; set; }

        [Key]
        public int HeatId { get; set; }
        [ForeignKey(nameof(HeatId))]
        public virtual Heat Heat { get; set; }

        [Key]
        public int TimeNumber { get; set; }

        public int RawTime { get; set; }
        public TimeType Type { get; set; }
        
    }
}
