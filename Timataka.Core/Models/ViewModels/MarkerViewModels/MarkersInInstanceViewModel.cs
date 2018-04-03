using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels.MarkerViewModels
{
    public class MarkersInInstanceViewModel
    {
        public Marker Marker { get; set; }
        public string EventName { get; set; }
        public int HeatNumber { get; set; }
    }
}
