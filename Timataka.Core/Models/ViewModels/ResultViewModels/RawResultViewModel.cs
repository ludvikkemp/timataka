using System;
using System.Collections.Generic;
using System.Text;

namespace Timataka.Core.Models.ViewModels.ResultViewModels
{
    public class RawResultViewModel
    {
        public string ChipCode { get; set; }
        public int CompetitionInstanceId { get; set; }
        public int Time01 { get; set; }
        public int Time02 { get; set; }
    }
}
