using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public enum Role
    {
        Host = 0,
        Official = 1,
        Staff = 2
    }

    public class ManagesCompetition
    {
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser ApplicationUserId { get; set; }

        public int CompetitionId { get; set; }
        [ForeignKey(nameof(CompetitionId))]
        public virtual Competition ApplicationCompetitonId { get; set; }

        public Role Role { get; set; }
    }
}
