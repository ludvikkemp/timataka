using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels.UserViewModels
{
    public class MyCompetitionsViewModel
    {
        public CompetitionInstance Instance { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}
