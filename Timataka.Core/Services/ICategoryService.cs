using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CategoryViewModels;

namespace Timataka.Core.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryViewModel> GetListOfCategoriesByEventId(int id);
        Task<bool> CategoryExistsAsync(string modelName);
        Task<Category> AddAsync(CategoryViewModel c);
        Task<Category> EditClubAsync(CategoryViewModel m);
        Task<CategoryViewModel> GetCategoryViewModelById(int id);
        Task<int> RemoveAsync(int categoryId);
    }
}
