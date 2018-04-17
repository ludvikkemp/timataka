using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.ViewModels.EventViewModels;

namespace Timataka.Core.Models.ViewModels.CompetitionViewModels
{
    /// <summary>
    /// Used for edit contestant
    /// </summary>
    public class EditContestantInCompetitionViewModel
    {
        //User
        public string UserId { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int Nationality { get; set; }
        public string Phone { get; set; }

        //Events
        public IEnumerable<ContestantInEventViewModel> Event { get; set; }


    }
}
