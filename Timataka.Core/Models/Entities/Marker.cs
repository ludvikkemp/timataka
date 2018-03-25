﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public enum Type { Gun = 0, Marker = 1 }

    public class Marker
    {
        [Key]
        public int EventId { get; set; }
        [ForeignKey(nameof(EventId))]
        public virtual Event Event { get; set; }

        [Key]
        public int HeatId { get; set; }
        [ForeignKey(nameof(HeatId))]
        public virtual Heat Heat { get; set; }

        public Type Type { get; set; }
        public int Time { get; set; }
        public string Location { get; set; }
    }
}
