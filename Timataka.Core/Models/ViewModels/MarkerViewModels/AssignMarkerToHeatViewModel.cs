using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Timataka.Core.Models.ViewModels.MarkerViewModels
{
    public class AssignMarkerToHeatViewModel
    {
        [Required]
        public int HeatId { get; set; }
        [Required]
        public int MarkerId { get; set; }
    }
}
