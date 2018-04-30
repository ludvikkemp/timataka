using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public class _Result
    {
        [Key]
        public int Pid { get; set; }
        public int? Lap { get; set; }
        public int CompetitionInstanceId { get; set; }
        public int? Time01 { get; set; }
        public int? Time02 { get; set; }
    }
}
