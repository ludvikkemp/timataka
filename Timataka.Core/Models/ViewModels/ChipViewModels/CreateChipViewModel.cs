using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Timataka.Core.Models.ViewModels.ChipViewModels
{
    public class CreateChipViewModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        [Range(0, 10000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Number { get; set; }
        public Boolean Active { get; set; }
    }
}
