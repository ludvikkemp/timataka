using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public int EventId { get; set; }
        [ForeignKey(nameof(EventId))]
        public virtual Event Event { get; set; }

        public int CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }

        public int AgeFrom { get; set; }
        public int AgeTo { get; set; }
        public Gender Gender { get; set; }
        public string Name { get; set; }
    }
}
