using System;
using System.Collections.Generic;
using System.Text;

namespace Timataka.Core.Models.ViewModels.AdminViewModels
{
    public class SportsViewModel
    {
        public bool IsSport { get; set; }
        public int SportId { get; set; }
        public string SportName { get; set; }
        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; }
    }
}
