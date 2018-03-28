using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timataka.Core.Data;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Services;
using Timataka.Core.Models.ViewModels.DisciplineViewModels;

namespace Timataka.Web.Controllers
{
    public class DisciplineController : Controller
    {
        private readonly IDisciplineService _disciplineService;

        public DisciplineController(IDisciplineService disciplineService)
        {
            _disciplineService = disciplineService;
        }

        //GET: /Admin/Sport/{sportId}/Discipline/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Sport/{sportId}/Discipline/Create")]
        public IActionResult Create(int sportId)
        {
            ViewBag.Sports = _disciplineService.GetSportsListItems();
            return View();
        }

        //POST: /Admin/Sport/{sportId}/Dicipline/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Sport/{sportId}/Discipline/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model, int sportId)
        {
            if (ModelState.IsValid)
            {
                var discipline = new Discipline
                {
                    Name = model.Name,
                    SportId = sportId
                };
                await _disciplineService.AddAsync(discipline);
            }
            return RedirectToAction("Sport", "Admin", new { @id = model.SportId });
        }

        //GET: /Admin/Sport/{sportId}/Discipline/Details/{disciplineId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("Admin/Sport/{sportId}/Discipline/Details/{disciplineId}")]
        public IActionResult Details(int disciplineId, int sportId)
        {
            var discipline = _disciplineService.GetDisciplineById(disciplineId);
            if (discipline == null)
            {
                return NotFound();
            }
            return View(discipline);
        }

        //GET: /Admin/Sport/{sportId}/Discipline/Edit/{disciplineId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("Admin/Sport/{sportId}/Discipline/Edit/{disciplineId}")]
        public IActionResult Edit(int disciplineId, int sportId)
        {
            var discipline = _disciplineService.GetDisciplineById(disciplineId);
            var model = new DisciplineViewModel
            {
                Id = discipline.Id,
                Name = discipline.Name,
                SportId = discipline.SportId

            };
            if (discipline == null)
            {
                return NotFound();
            }
            return View(model);
        }

        //POST: /Admin/Sport/{sportId}/Discipline/Edit/{disciplineId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Admin/Sport/{sportId}/Discipline/Edit/{disciplineId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int sportId, int disciplineId, DisciplineViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _disciplineService.GetDisciplineById(model.Id);
                result.Name = model.Name;
                await _disciplineService.EditAsync(result);
                return RedirectToAction("Sport", "Admin", new { @id = sportId });
            }

            return View(model);
        }

        //Get: /Admin/Sport/{sportId}/Discipline/Delete/{disciplineId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Sport/{sportId}/Discipline/Delete/{disciplineId}")]
        public IActionResult Delete(int sportId, int disciplineId)
        {
            var discipline = _disciplineService.GetDisciplineById(disciplineId);
            return View(discipline);
        }

        //Post: /Admin/Sport/{sportId}/Discipline/Delete/{disciplineId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Sport/{sportId}/Discipline/Delete/{disciplineId}")]
        public IActionResult DeleteConfirmed(int sportId, int disciplineId)
        {
            _disciplineService.Remove(disciplineId);
            return RedirectToAction("Sport", "Admin", new { @sportId = sportId});
        }
    }
}