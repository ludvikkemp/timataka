using System;
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
        public int Id { get; set; }

        public int CompetitionInstanceId { get; set; }
        [ForeignKey(nameof(CompetitionInstanceId))]
        public virtual CompetitionInstance CompetitionInstance { get; set; }

        public Type Type { get; set; }
        public int Time { get; set; }
        public string Location { get; set; }
    }
}
