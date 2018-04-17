using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels
{
    public class CreateResultViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int HeatId { get; set; }
        public ResultStatus Status { get; set; }
        public string FinalTime { get; set; }
        public string Notes { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
