using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Timataka.Core.Models.ViewModels.ChipViewModels
{
    public class ChipViewModel
    {
        [Required]
        [Display(Name = "Chip Code", Description = "7 digit code")]
        public string Code { get; set; }
        [Required]
        [Display(Name = "Chip Number")]
        public int Number { get; set; }
        public Boolean Active { get; set; }

        public string LastUserId { get; set; }
        public string LastUserName { get; set; }
        public string LastUserSsn { get; set; }
        public int LastCompetitionInstanceId { get; set; }
        public string LastCompetitionInstanceName { get; set; }
        public DateTime LastSeen { get; set; }
    }
}
