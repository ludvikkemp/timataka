using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Dto.CompetitionInstanceDTO;

namespace Timataka.Core.Models.ViewModels.HomeViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<LatestResultsDTO> LatestAthleticsResults { get; set; }
        public IEnumerable<LatestResultsDTO> UpcomingAthleticsEvents { get; set; }

        public IEnumerable<LatestResultsDTO> LatestCyclingResults { get; set; }
        public IEnumerable<LatestResultsDTO> UpcomingCyclingEvents { get; set; }

        public IEnumerable<LatestResultsDTO> LatestOtherResults { get; set; }
        public IEnumerable<LatestResultsDTO> UpcomingOtherEvents { get; set; }
    }
}
