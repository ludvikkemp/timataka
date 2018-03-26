using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.ViewModels.CourseViewModels;
using Timataka.Core.Services;

namespace Timataka.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IDisciplineService _disciplineService;
        public CourseController(ICourseService courseService, IDisciplineService disciplineService)
        {
            _courseService = courseService;
            _disciplineService = disciplineService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Disciplines = _disciplineService.GetAllDisciplines();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel model)
        {
            if (ModelState.IsValid && model.Name != null)
            {
                var exists = await _courseService.CourseExistsAsync(model.Name);
                if (!exists)
                {
                    await _courseService.AddAsync(model);
                    return RedirectToAction("Courses", "Admin");
                }
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Disciplines = _disciplineService.GetAllDisciplines();
            var model = _courseService.GetCourseViewModelById(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var success = await _courseService.EditCourseAsync(model);
                if (success)
                {
                    return RedirectToAction("Courses", "Admin");
                }   
            }
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var course = _courseService.GetCourseViewModelById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        public IActionResult Delete(int id)
        {
            var model = _courseService.GetCourseViewModelById(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CourseViewModel model)
        {
            var success = await _courseService.MarkCourseAsDeleted(id);
            if (success)
            {
                return RedirectToAction("Courses", "Admin");
            }
            return View();
        }
    }
}