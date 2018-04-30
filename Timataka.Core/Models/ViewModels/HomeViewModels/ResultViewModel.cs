using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels.HomeViewModels
{
    public class ResultViewModel
    {
        //User Data
        public string UserId { get; set; }
        public DateTime DateOfBirth { get; set; }

        //Heat Data
        public int HeatId { get; set; }
        public int HeatNumber { get; set; }

        // ContestantInHeat Data
        public int Bib { get; set; }

        // Result Data
        public string Country { get; set; }
        public string Nationality { get; set; }
        public ResultStatus Status { get; set; }
        public string GunTime { get; set; }
       
        public string Gender { get; set; }
        public string Name { get; set; }
        public string Club { get; set; }
        public string Notes { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string ChipCode { get; set; }

        //Extra data
        public int RawGunTime { get; set; }
        public string ChipTime { get; set; }
        public int Rank { get; set; }

    }
}
