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
        private readonly IEventService _eventService;

        public CourseController(ICourseService courseService,
                                IDisciplineService disciplineService,
                                IEventService eventService)
        {
            _courseService = courseService;
            _disciplineService = disciplineService;
            _eventService = eventService;
        }

        //GET: /Admin/Course/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Course/Create")]
        public IActionResult Create()
        {
            ViewBag.Disciplines = _disciplineService.GetAllDisciplines();
            return View();
        }

        //POST: /Admin/Course/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Course/Create")]
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

        //GET: /Admin/Club/Edit/{courseId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Course/Edit/{courseId}")]
        public IActionResult Edit(int courseId)
        {
            ViewBag.Disciplines = _disciplineService.GetAllDisciplines();
            var model = _courseService.GetCourseViewModelById(courseId);
            if (model == null)
            {
                return NotFound();
            }
            var exists = (from e in _eventService.GetAllEvents()
                          where e.CourseId == courseId
                          select e).Count();
            if (exists > 0)
            {
                return Json("An event is using this course and therefore edit not allowed. Please go back and create a new course");
            }
            return View(model);
        }

        //POST: /Admin/Club/Edit/{courseId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Course/Edit/{courseId}")]
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

        //GET: /Admin/Club/Details{courseId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Course/Details/{courseId}")]
        public IActionResult Details(int courseId)
        {
            var course = _courseService.GetCourseViewModelById(courseId);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        //GET: /Admin/Course/Delete{courseId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Course/Delete/{courseId}")]
        public IActionResult Delete(int courseId)
        {
            var model = _courseService.GetCourseViewModelById(courseId);
            if (model == null)
            {
                return NotFound();
            }
            var exists = (from e in _eventService.GetAllEvents()
                          where e.CourseId == courseId
                          select e).Count();
            if (exists > 0)
            {
                return Json("An event is using this course and therefore delete not allowed.");
            }
            return View(model);
        }

        //POST: /Admin/Course/Delete/{courseId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Course/Delete/{courseId}")]
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