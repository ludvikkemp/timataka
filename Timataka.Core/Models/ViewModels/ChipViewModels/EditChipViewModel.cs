using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Timataka.Core.Models.ViewModels.ChipViewModels
{
    public class EditChipViewModel
    {
        public string Code { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public Boolean Active { get; set; }
    }
}
