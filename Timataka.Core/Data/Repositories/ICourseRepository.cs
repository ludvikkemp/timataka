using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CourseViewModels;

namespace Timataka.Core.Data.Repositories
{
    public interface ICourseRepository
    {
        void Insert(Course c);
        Task InsertAsync(Course c);
        IEnumerable<Course> Get();
        Course GetById(int id);
        Task<Course> GetByIdAsync(int id);
        void Edit(Course c);
        Task EditAsync(Course c);
        void Remove(Course c);
        Task RemoveAsync(Course c);
        Task<Course> GetCourseByNameAsync(string cName);
        IEnumerable<CourseViewModel> GetCourseViewModels();
    }
}
