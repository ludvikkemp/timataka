using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Dto.CompetitionInstanceDTO;

namespace Timataka.Core.Models.Dto.ResultsDTO
{
    public class ResultsDTO
    {
        public IEnumerable<LatestResultsDTO> All { get; set; }
        public IEnumerable<LatestResultsDTO> Athletics { get; set; }
        public IEnumerable<LatestResultsDTO> Cycling { get; set; }
        public IEnumerable<LatestResultsDTO> Other { get; set; }
    }
}
