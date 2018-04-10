using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.Dto.CompetitionInstanceDTO
{
    public class MarkersDto
    {
        public IEnumerable<Marker> Markers { get; set; }
        public string CompetitionName { get; set; }
        public string CompetitionInstanceName { get; set; }
    }
}
