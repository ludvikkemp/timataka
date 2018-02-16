using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    class Chip
    {
        public string ChipCode { get; set; }
        public int ChipNumber { get; set; }
        public Boolean Active { get; set; }
        [ForeignKey("ApplicationUser")]
        public int LastUserId { get; set; }
        [ForeignKey("Event")]
        public int LastEventId { get; set; }
        public DateTime LastSeen { get; set; }
    }
}
