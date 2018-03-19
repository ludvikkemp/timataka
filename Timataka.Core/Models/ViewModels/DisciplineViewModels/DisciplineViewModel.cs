using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Timataka.Core.Models.ViewModels.DisciplineViewModels
{
    public class DisciplineViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int SportId { get; set; }
    }
}
