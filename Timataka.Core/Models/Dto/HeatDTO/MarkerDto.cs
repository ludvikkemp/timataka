using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.MarkerViewModels;

namespace Timataka.Core.Models.Dto.HeatDTO
{
    public class MarkerDto
    {
        public IEnumerable<Marker> AssignedMarkers { get; set; }
        public IEnumerable<Marker> MarkerList { get; set; }
        public AssignMarkerToHeatViewModel AssingnMarkerToHeatViewModel { get; set; }

        // Breadcrumbs
        public string CompetitionName { get; set; }
        public string CompetitionInstanceName { get; set; }
        public string EventName { get; set; }
        public int HeatNumber { get; set; }
    }
}
