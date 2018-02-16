using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public enum ClubRole { Member = 0, Coach = 1, Manager = 2 };
    public class UserInClub
    {
        [Key]
        [Column(Order = 0)]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser ApplicationUserId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int SportId { get; set; }
        [ForeignKey(nameof(SportId))]
        public virtual Sport ApplicationSportId { get; set; }

        public int ClubId { get; set; }
        [ForeignKey(nameof(ClubId))]
        public virtual Sport ApplicationClubId { get; set; }

        public ClubRole Role { get; set; }
    }
}
