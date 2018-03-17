using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Timataka.Core.Models.ViewModels.HeatViewModels
{
    public class CreateHeatViewModel
    {
        [Required]
        public int EventId { get; set; }

    }
}
