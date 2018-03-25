using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public class DevicesInEvent
    {
        [Key]
        public int DeviceId { get; set; }
        [ForeignKey(nameof(DeviceId))]
        public virtual Device Device { get; set; }

        [Key]
        public int EventId { get; set; }
        [ForeignKey(nameof(EventId))]
        public virtual Event Event { get; set; }
    }
}