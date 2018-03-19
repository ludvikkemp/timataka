using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public class Heat
    {
        [Key]
        public int Id { get; set; }
        public int EventId { get; set; }
        [ForeignKey(nameof(EventId))]
        public virtual Event Event { get; set; }
        public int HeatNumber { get; set; }
        public Boolean Deleted { get; set; }

    }
}
