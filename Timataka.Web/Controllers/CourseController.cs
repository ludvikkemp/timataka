using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.ViewModels.CourseViewModels;
using Timataka.Core.Services;

namespace Timataka.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IDisciplineService _disciplineService;

        public CourseController(ICourseService courseService,
                                IDisciplineService disciplineService)
        {
            _courseService = courseService;
            _disciplineService = disciplineService;
        }

        // Get: Admin/Courses/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("Admin/Courses/Create")]
        public IActionResult Create()
        {
            ViewBag.Disciplines = _disciplineService.GetAllDisciplines();
            return View();
        }

        // Post: Admin/Courses/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Admin/Courses/Create")]
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

        // Get: Admin/Courses/Edit/{courseId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("Admin/Courses/Edit/{courseId}")]
        public IActionResult Edit(int courseId)
        {
            ViewBag.Disciplines = _disciplineService.GetAllDisciplines();
            var model = _courseService.GetCourseViewModelById(courseId);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // Post: Admin/Courses/Edit/{courseId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Admin/Courses/Edit/{courseId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int courseId, CourseViewModel model)
        {
            if (courseId != model.Id)
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

        // Get: Admin/Courses/Details/{courseId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("Admin/Courses/Details/{courseId}")]
        public IActionResult Details(int courseId)
        {
            var course = _courseService.GetCourseViewModelById(courseId);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // Get: Admin/Courses/Delete/{courseId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("Admin/Courses/Delete/{courseId}")]
        public IActionResult Delete(int courseId)
        {
            var model = _courseService.GetCourseViewModelById(courseId);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // Post: Admin/Courses/Delete/{courseId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Admin/Courses/Delete/{courseId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int courseId, CourseViewModel model)
        {
            var success = await _courseService.MarkCourseAsDeleted(courseId);
            if (success)
            {
                return RedirectToAction("Courses", "Admin");
            }
            return View();
        }
    }
}