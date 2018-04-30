using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Dto.CompetitionInstanceDTO;

namespace Timataka.Core.Models.ViewModels.HomeViewModels
{
    public class DisplayResultsViewModel
    {
        public IEnumerable<LatestResultsDTO> LatestResults { get; set; }
        public IEnumerable<LatestResultsDTO> UpcomingEvents { get; set; }
    }
}
