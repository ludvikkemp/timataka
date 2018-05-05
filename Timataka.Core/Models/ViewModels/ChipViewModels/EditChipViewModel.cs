using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Timataka.Core.Models.ViewModels.ChipViewModels
{
    public class EditChipViewModel
    {
        [Display(Name ="Chip Code", Description ="7 digit code")]
        public string Code { get; set; }
        [Required]
        [Display(Name ="Chip Number")]
        public int Number { get; set; }
        [Required]
        public Boolean Active { get; set; }
    }
}
