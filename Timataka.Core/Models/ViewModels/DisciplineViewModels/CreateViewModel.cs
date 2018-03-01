using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Timataka.Core.Models.ViewModels.DisciplineViewModels
{
    public class CreateViewModel
    {
        [Required]
        [Display(Name = "Discipline")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "SportId")]
        public string SportId { get; set; }
    }
}
