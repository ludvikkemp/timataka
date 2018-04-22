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
        private readonly IEventRepository _eventRepo;
        private readonly IAccountRepository _accountRepo;
        public CategoryService(
            ICategoryRepository repo,
            IEventRepository eventRepo,
            IAccountRepository accountRepo)
        {
            _repo = repo;
            _eventRepo = eventRepo;
            _accountRepo = accountRepo;
        }

        public IEnumerable<CategoryViewModel> GetListOfCategoriesByEventId(int id)
        {
            return _repo.GetListOfCategoriesForEvent(id);
        }

        public async Task<bool> CategoryExistsAsync(string modelName)
        {
            var result = await _repo.GetCategoryByNameAsync(modelName);
            if (result != null) return true;
            return false;
        }

        public async Task<Category> AddAsync(CategoryViewModel c)
        {
            if (c.CountryId == 0)
            {
                c.CountryId = null;
            }
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

        public async Task<Category> EditCategoryAsync(CategoryViewModel m)
        {
            if (m.CountryId == 0)
            {
                m.CountryId = null;
            }
            var c = await _repo.GetByIdAsync(m.Id);
            c.Name = m.Name;
            c.AgeFrom = m.AgeFrom;
            c.AgeTo = m.AgeTo;
            c.CountryId = m.CountryId;
            c.EventId = m.EventId;
            c.Gender = m.Gender;
            await _repo.EditAsync(c);
            return c;
        }

        public async Task<CategoryViewModel> GetCategoryViewModelById(int id)
        {
            var c = _repo.GetById(id);
            var e = await _eventRepo.GetEventByIdAsync(c.EventId);
            string n;
            if (c.CountryId == null)
            {
                n = "All";
            }
            else
            {
                n = _accountRepo.GetCountryById((int)c.CountryId).Name;
            }
            
            var model = new CategoryViewModel
            {
                EventName = e.Name,
                CountryName = n,
                Name = c.Name,
                AgeFrom = c.AgeFrom,
                AgeTo = c.AgeTo,
                CountryId = c.CountryId,
                EventId = c.EventId,
                Gender = c.Gender,
                Id = c.Id
            };
            return model;
        }

        public Task<int> RemoveAsync(int categoryId)
        {
            var entity = _repo.GetById(categoryId);
            return _repo.RemoveAsync(entity);
        }
    }
}
