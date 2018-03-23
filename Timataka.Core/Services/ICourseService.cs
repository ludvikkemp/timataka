using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.ViewModels.CourseViewModels;

namespace Timataka.Core.Services
{
    public interface ICourseService
    {
        IEnumerable<CourseViewModelDropDownList> GetCourseDropDown();
    }
}
