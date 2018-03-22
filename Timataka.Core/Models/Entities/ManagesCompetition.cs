using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public enum CompetitionRole
    {
        Host = 0,
        Official = 1,
        Staff = 2
    }

    public class ManagesCompetition
    {
        [Key]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser ApplicationUserId { get; set; }

        [Key]
        public int CompetitionId { get; set; }
        [ForeignKey(nameof(CompetitionId))]
        public virtual Competition ApplicationCompetitonId { get; set; }

        public CompetitionRole Role { get; set; }
    }
}
