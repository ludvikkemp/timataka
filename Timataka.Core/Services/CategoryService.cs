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

        public async Task<bool> CategoryExistsAsync(string modelName)
        {
            var result = await _repo.GetCategoryByNameAsync(modelName);
            if (result != null) return true;
            return false;
        }

        public async Task<Category> AddAsync(CategoryViewModel c)
        {
            var newCategory = new Category
            {
                Name = c.Name,
                AgeFrom = c.AgeFrom,
                AgeTo = c.AgeTo,
                CountryId = c.CountryId,
                EventId = c.EventId,
                Gender = c.Gender
            };
            await _repo.InsertAsync(newCategory);
            return newCategory;
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
