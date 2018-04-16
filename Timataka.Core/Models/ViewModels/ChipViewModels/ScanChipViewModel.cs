using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Timataka.Core.Models.ViewModels.ChipViewModels
{
    public class ScanChipViewModel
    {
        [Required]
        public string Code { get; set; }
    }
}
