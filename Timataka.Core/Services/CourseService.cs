using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CourseViewModels;

namespace Timataka.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repo;
        private readonly IDisciplineRepository _disciplineRepo;

        public CourseService(ICourseRepository repo, IDisciplineRepository disciplineRepo)
        {
            _repo = repo;
            _disciplineRepo = disciplineRepo;
        }

        public IEnumerable<CourseViewModelDropDownList> GetCourseDropDown(int? disciplineId = null)
        {
            var courses = (from c in _repo.Get()
                           where c.Deleted == false
                          select c).ToList();
            if (disciplineId != null)
            {
                courses = (from c in courses
                           where c.DisciplineId == disciplineId
                           select c).ToList();
            }
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

        public CourseViewModel GetCourseViewModelById(int id)
        {
            var c = _repo.GetById(id);
            var d = _disciplineRepo.GetById(c.DisciplineId);
            var model = new CourseViewModel
            {
                Name = c.Name,
                Id = c.Id,
                Lap = c.Lap,
                ExternalCourseId = c.ExternalCourseId,
                Distance = c.Distance,
                DisciplineId = c.DisciplineId,
                DisciplineName = d.Name,
                Deleted = c.Deleted
            };
            return model;
        }

        public async Task<bool> EditCourseAsync(CourseViewModel model)
        {
            var c = await _repo.GetByIdAsync(model.Id);
            if (c != null)
            {
                c.Name = model.Name;
                c.DisciplineId = model.DisciplineId;
                c.ExternalCourseId = model.ExternalCourseId;
                c.Lap = model.Lap;
                c.Distance = model.Distance;
                await _repo.EditAsync(c);
                return true;
            }
            return false;
        }

        public async Task<bool> MarkCourseAsDeleted(int modelId)
        {
            var c = await _repo.GetByIdAsync(modelId);
            if (c != null)
            {
                c.Deleted = true;
                await _repo.EditAsync(c);
                return true;
            }
            return false;
        }

        public IEnumerable<CourseViewModel> GetListOfCourseViewModels()
        {
            return _repo.GetCourseViewModels();
        }
    }
}
