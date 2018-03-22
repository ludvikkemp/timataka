﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private bool _disposed = false;
        private readonly ApplicationDbContext _db;

        public CourseRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Insert(Course c)
        {
            _db.Courses.Add(c);
            _db.SaveChanges();
        }

        public async Task InsertAsync(Course c)
        {
            await _db.Courses.AddAsync(c);
            await _db.SaveChangesAsync();
        }

        public IEnumerable<Course> Get()
        {
            return _db.Courses.ToList();
        }

        public Course GetById(int id)
        {
            return _db.Courses.SingleOrDefault(x => x.Id == id);
        }

        public Task<Course> GetByIdAsync(int id)
        {
            return _db.Courses.SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Edit(Course c)
        {
            _db.Courses.Update(c);
            _db.SaveChanges();
        }

        public async Task EditAsync(Course c)
        {
            _db.Courses.Update(c);
            await _db.SaveChangesAsync();
        }

        public void Remove(Course c)
        {
            //TODO:Mark as removed, not delete compleatly
            _db.Courses.Update(c);
            _db.SaveChanges();
        }

        public async Task RemoveAsync(Course c)
        {
            //TODO:Mark as removed, not delete compleatly
            _db.Courses.Update(c);
            await _db.SaveChangesAsync();
        }

        public Task<Course> GetCompetitionByNameAsync(string cName)
        {
            return _db.Courses.SingleOrDefaultAsync(x => x.Name == cName);
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
