using System;
using System.Collections.Generic;
using System.Text;

namespace Timataka.Core.Models.ViewModels.HeatViewModels
{
    public class ContestantsInHeatViewModel
    {
        public string UserId { get; set; }
        public int HeatId { get; set; }
        public int Bib { get; set; }
        public string Team { get; set; }
        public DateTime Modified { get; set; }

        //USER INFO
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Ssn { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public bool DeletedUser { get; set; }
        public int? CountryId { get; set; }
        public int? NationalityId { get; set; }


        //HEAT INFO
        public int EventId { get; set; }
        public int HeatNumber { get; set; }
        public Boolean DeletedHeat { get; set; }
    }
}
