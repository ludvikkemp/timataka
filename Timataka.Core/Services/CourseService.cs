using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Data;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CourseViewModels;

namespace Timataka.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repo;

        public CourseService(ICourseRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<CourseViewModelDropDownList> GetCourseDropDown()
        {
            var courses = _repo.Get();
            var dropDownList = courses.Select(course => new CourseViewModelDropDownList
                {
                    Id = course.Id,
                    Name = course.Name
                })
                .ToList();
            return dropDownList;
        }

        public async Task<bool> CourseExistsAsync(string modelName)
        {
            var result = await _repo.GetCourseByNameAsync(modelName);
            if (result != null) return true;
            return false;
        }

        public async Task AddAsync(CourseViewModel model)
        {
            var newCourse = new Course
            {
                Name = model.Name,
                DisciplineId = model.DisciplineId,
                Distance = model.Distance,
                Lap = model.Lap,
                ExternalCourseId = model.ExternalCourseId
            };
            await _repo.InsertAsync(newCourse);
        }

        public IEnumerable<Course> GetListOfCourses()
        {
            var courses = _repo.Get();
            return courses;
        }
    }
}
