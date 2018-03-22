using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timataka.Core.Data;
using Timataka.Core.Data.Repositories;
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
    }
}
