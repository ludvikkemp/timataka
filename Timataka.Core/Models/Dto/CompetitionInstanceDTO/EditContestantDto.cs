using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.ClubViewModels;
using Timataka.Core.Models.ViewModels.ContestantViewModels;
using Timataka.Core.Models.ViewModels.EventViewModels;

namespace Timataka.Core.Models.Dto.CompetitionInstanceDTO
{
    public class EditContestantDto
    {
        public IEnumerable<ContestantInEventViewModel> Events { get; set; }

        public string ContestantName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public int? NationId { get; set; }
        public string Phone { get; set; }
    }

}
