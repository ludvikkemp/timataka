using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.ViewModels.CategoryViewModels;

namespace Timataka.Core.Models.Dto.AdminDTO
{
    public class CategoryDto
    {
        public IEnumerable<CategoryViewModel> CategoryViewModels { get; set; }
        
        // BreadCrumbs
        public string CompetitionName { get; set; }
        public string CompetitonInstanceName { get; set; }
        public string EventName { get; set; }
    }
}
