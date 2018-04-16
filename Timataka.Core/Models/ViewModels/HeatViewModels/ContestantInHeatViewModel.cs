using System;
using System.Collections.Generic;
using System.Text;

namespace Timataka.Core.Models.ViewModels.HeatViewModels
{
    public class ContestantInHeatViewModel
    {
        public string UserId { get; set; }
        public int Bib { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Ssn { get; set; }
        public int HeatId { get; set; }
        public int HeatNumber { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Team { get; set; }
        public string Gender { get; set; }
    }
}
