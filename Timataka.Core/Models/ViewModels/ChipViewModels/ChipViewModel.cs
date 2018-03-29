using System;
using System.Collections.Generic;
using System.Text;

namespace Timataka.Core.Models.ViewModels.ChipViewModels
{
    public class ChipViewModel
    {
        public string Code { get; set; }
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
