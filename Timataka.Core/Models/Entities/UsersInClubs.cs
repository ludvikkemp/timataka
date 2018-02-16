using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    class UsersInClubs
    {
        public enum _Role {Member=0,Coach=1,Manager=2};

        [ForeignKey("ApplicationUser")]
        public int UserId { get; set; }
        [ForeignKey("Sport")]
        public int SportId { get; set; }
        [ForeignKey("Club")]
        public int ClubId { get; set; }
        public _Role Role { get; set; }
    }
}
