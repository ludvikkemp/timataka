﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CategoryViewModels;

namespace Timataka.Core.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        private bool _disposed;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Insert(Category c)
        {
            _db.Categories.Add(c);
            _db.SaveChanges();
        }

        public async Task InsertAsync(Category c)
        {
            await _db.Categories.AddAsync(c);
            await _db.SaveChangesAsync();
        }

        public IEnumerable<Category> Get()
        {
            return _db.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return _db.Categories.SingleOrDefault(x => x.Id == id);
        }

        public Task<Category> GetByIdAsync(int id)
        {
            return _db.Categories.SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Edit(Category c)
        {
            _db.Categories.Update(c);
            _db.SaveChanges();
        }

        public async Task EditAsync(Category c)
        {
            _db.Categories.Update(c);
            await _db.SaveChangesAsync();
        }

        public void Remove(Category c)
        {
            _db.Categories.Remove(c);
            _db.SaveChanges();
        }

        public async Task<int> RemoveAsync(Category c)
        {
            _db.Categories.Remove(c);
            await _db.SaveChangesAsync();
            return 1;
        }

        public Task<Category> GetCategoryByNameAsync(string cName)
        {
            return _db.Categories.SingleOrDefaultAsync(x => x.Name == cName);
        }

        public IEnumerable<CategoryViewModel> GetListOfCategoriesForEvent(int eventId)
        {
            var categories = (from c in _db.Categories
                join e in _db.Events on c.EventId equals e.Id
                join country in _db.Countries on c.CountryId equals country.Id into cc
                from country in cc.DefaultIfEmpty()
                where c.EventId == eventId
                select new CategoryViewModel
                {
                    EventId = e.Id,
                    Id = c.Id,
                    AgeFrom = c.AgeFrom,
                    AgeTo = c.AgeTo,
                    CountryId = c.CountryId.GetValueOrDefault(0),
                    CountryName = country.Name ?? "All",
                    Name = c.Name,
                    EventName = e.Name,
                    Gender = c.Gender
                }).ToList();
            return categories;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }

            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }

}
