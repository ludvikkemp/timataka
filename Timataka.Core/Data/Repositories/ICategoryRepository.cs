using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CategoryViewModels;

namespace Timataka.Core.Data.Repositories
{
    public interface ICategoryRepository : IDisposable
    {
        IEnumerable<CategoryViewModel> GetListOfCategoriesForEvent(int eventId);
        void Insert(Category c);
        Task InsertAsync(Category c);
        IEnumerable<Category> Get();
        Category GetById(int id);
        Task<Category> GetByIdAsync(int id);
        void Edit(Category c);
        Task EditAsync(Category c);
        void Remove(Category c);
        Task RemoveAsync(Category c);
        Task<Category> GetCategoryByNameAsync(string cName);
    }
}
