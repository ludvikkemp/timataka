using System;
using System.Collections.Generic;
using System.Text;

namespace Timataka.Core.Models.ViewModels.EventViewModels
{
    public class AddContestantToEventViewModel
    {
        public string UserId { get; set; }
        public ContestantInEventViewModel ContestantInEvent { get; set; }
    }
}
