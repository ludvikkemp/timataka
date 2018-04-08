using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;

namespace Timataka.Core.Models.Dto.CompetitionInstanceDTO
{
    public class ContestantsInCompetitionInstanceDTO
    {
        public IEnumerable<ContestantsInCompetitionViewModel> Contestants { get; set; }
        public Competition Competition { get; set; }
        public CompetitionInstance CompetitionInstance { get; set; }


    }
}
