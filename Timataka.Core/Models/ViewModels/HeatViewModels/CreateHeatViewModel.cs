using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Timataka.Core.Models.ViewModels.HeatViewModels
{
    public class CreateViewModel
    {
        [Required]
        public int EventId { get; set; }

        [Required]
        public int HeatNumber { get; set; }
    }
}
