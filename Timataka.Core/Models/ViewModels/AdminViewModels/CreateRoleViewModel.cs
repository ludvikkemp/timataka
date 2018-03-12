using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Cache;
using System.Text;

namespace Timataka.Core.Models.ViewModels.AdminViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
