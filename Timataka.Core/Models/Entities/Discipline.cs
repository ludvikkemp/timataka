using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public class Discipline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int SportId { get; set; }
        [ForeignKey(nameof(SportId))]
        public virtual Sport ApplicationSportId { get; set; }

        public Boolean Deleted { get; set; }
    }
}
