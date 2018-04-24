using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public class _Marker
    {
        [Key]
        public int Id { get; set; }
        public string Device { get; set; }
        public string Location { get; set; }
        public string Marker { get; set; }
        public DateTime MarkerTime { get; set; }
        public int MilliSecs { get; set; }
        public string Type { get; set; }
    }
}
