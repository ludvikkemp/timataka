using System;
using System.Collections.Generic;
using System.Text;

namespace Timataka.Core.Models.Dto.CompetitionInstanceDTO
{
    public class LatestResultsDTO
    {
        public int CompetitionInstanceId { get; set; }
        public int CompetitionId { get; set; }
        public string CompetitionName { get; set; }
        public string CompetitionInstanceName { get; set; }
        public int NumberOfContestants { get; set; }
        public DateTime Date { get; set; }
        public Boolean Live { get; set; }
    }
}
