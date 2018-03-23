using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels.CompetitionViewModels
{
    public class ManagesCompetitionViewModel
    {
        public string UserId { get; set; }   
        public int CompetitionId { get; set; }
        public Role Role { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Ssn { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool UserDeleted { get; set; }

        public bool Deleted { get; set; } = false;
    }
}
