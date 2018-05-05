using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels.MarkerViewModels
{
    public class MarkersInEventViewModel
    {
        public Marker Marker { get; set; }
        [Display(Name="Heat Number")]
        public int HeatNumber { get; set; }
    }
}
