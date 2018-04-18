﻿using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels.HomeViewModels
{
    public class ResultViewModel
    {
        //User Data
        public string UserId { get; set; }
        
        //Heat Data
        public int HeatId { get; set; }
        public int HeatNumber { get; set; }

        // Result Data
        public string Country { get; set; }
        public string Nationality { get; set; }
        public ResultStatus Status { get; set; }
        public string FinalTime { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public string Club { get; set; }
        public string Notes { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        
    }
}