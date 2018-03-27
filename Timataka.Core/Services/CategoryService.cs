using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CategoryViewModels;

namespace Timataka.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Category> GetListOfCategories()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CategoryExistsAsync(string modelName)
        {
            throw new NotImplementedException();
        }

        public Task<Category> AddAsync(CategoryViewModel c)
        {
            throw new NotImplementedException();
        }

        public Task<Category> EditClubAsync(CategoryViewModel m)
        {
            throw new NotImplementedException();
        }

        public CategoryViewModel GetCategoryViewModelById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveAsync(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
