﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Timataka.Core.Models.ViewModels.AdminViewModels
{
    public class SportsViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
