using System;
using System.Collections.Generic;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public class _Time
    {
        public int Antenna { get; set; }
        public string Chip { get; set; }
        public int ChipTime { get; set; }
        public int ChipType { get; set; }
        public int LapRaw { get; set; }
        public string Location { get; set; }
        public int MilliSecs { get; set; }
        public int PC { get; set; }
        public int Reader { get; set; }

    }
}
