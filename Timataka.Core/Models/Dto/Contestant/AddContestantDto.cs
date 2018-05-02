using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.ViewModels.ContestantViewModels;

namespace Timataka.Core.Models.Dto.Contestant
{
    public class AddContestantDto
    {
        public List<AddContestantViewModel> AddContestantViewModels { get; set; }
        public string FullName { get; set; }
    }
}
