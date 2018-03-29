using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public class Chip
    {
        [Key]
        public string Code { get; set; }
        public int Number { get; set; }
        public Boolean Active { get; set; }

        public string LastUserId { get; set; }
        [ForeignKey(nameof(LastUserId))]
        public virtual ApplicationUser User { get; set; }

        public int LastCompetitionInstanceId { get; set; }
        [ForeignKey(nameof(LastCompetitionInstanceId))]
        public virtual CompetitionInstance CompetitionInstance { get; set; }

        public DateTime LastSeen { get; set; }
    }
}
