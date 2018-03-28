using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels.Marker
{
    public class MarkerCreateViewModel
    {
        [Required]
        public int CompetitionInstanceId { get; set; }
        public int? HeatId { get; set; }
        public Entities.Type Type { get; set; }
        public int Time { get; set; }
        public string Location { get; set; }
    }
}
