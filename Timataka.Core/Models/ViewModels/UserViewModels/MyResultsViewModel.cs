using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels.UserViewModels
{
    public class MyResultsViewModel
    {
        //User Data
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }

        // ContestantInHeat Data
        public int Bib { get; set; }

        //Heat Data
        public int HeatId { get; set; }
        public int HeatNumber { get; set; }

        // Event Data
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDateFrom { get; set; }
        public DateTime EventDateTo { get; set; }

        // Instance Data
        public int CompetitionInstanceId { get; set; }
        public string CompetitionInstanceName { get; set; }
        public Status CompetitionInstanceStatus { get; set; }

        // Discipline Data
        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; }
        public int SportId { get; set; }

        // Course Data
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int CourseDistance { get; set; }

        // Result Data
        public string Country { get; set; }
        public string Nationality { get; set; }
        public ResultStatus Status { get; set; }
        public string GunTime { get; set; }
        public string ChipTime { get; set; }
        public string Gender { get; set; }
        public string Club { get; set; }
        public string Notes { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public string ChipCode { get; set; }
        public int Rank { get; set; }
        public int RawGunTime { get; set; }
    }
}
