using System;
using System.Collections.Generic;
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
        public int UserId { get; set; }
        public int CompetitionId { get; set; }
        public Role Role { get; set; }
    }
}
