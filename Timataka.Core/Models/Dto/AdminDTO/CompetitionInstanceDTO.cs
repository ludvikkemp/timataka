using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.EventViewModels;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;

namespace Timataka.Core.Models.Dto.AdminDTO
{
    public class CompetitionInstanceDTO
    {
        public Competition Competition { get; set; }
        public CompetitionsInstanceViewModel CompetitionInstance { get; set; }
        public IEnumerable<EventViewModel> Events { get; set; }
    }
}
