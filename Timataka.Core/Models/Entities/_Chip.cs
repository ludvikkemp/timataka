using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public class _Chip
    {
        [Key]
        public string Chip { get; set; }
        public int Pid { get; set; }
    }
}
