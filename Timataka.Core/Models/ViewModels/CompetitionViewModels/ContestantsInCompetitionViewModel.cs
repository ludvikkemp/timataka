using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.EventViewModels;

namespace Timataka.Core.Models.ViewModels.CompetitionViewModels
{
    public class ContestantsInCompetitionViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public IEnumerable<EventDropDownListViewModel> EventList { get; set; }
    }
}
