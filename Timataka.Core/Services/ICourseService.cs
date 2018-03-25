using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CourseViewModels;

namespace Timataka.Core.Services
{
    public interface ICourseService
    {
        IEnumerable<CourseViewModelDropDownList> GetCourseDropDown();
        Task<bool> CourseExistsAsync(string modelName);
        Task AddAsync(CourseViewModel model);
        IEnumerable<Course> GetListOfCourses();
    }
}
