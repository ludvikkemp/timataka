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
        private readonly ISportService _sportService;

        public DisciplineController(IDisciplineService disciplineService, 
                                    ISportService sportService)
        {
            _disciplineService = disciplineService;
            _sportService = sportService;
        }

        //GET: /Admin/Sport/{sportId}/Discipline/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Sport/{sportId}/Discipline/Create")]
        public IActionResult Create(int sportId)
        {
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

        
        //GET: /Admin/Sport/{sportId}/Discipline/Edit/{disciplineId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Sport/{sportId}/Discipline/Edit/{disciplineId}")]
        public async Task<IActionResult> Edit(int disciplineId, int sportId)
        {
            var discipline = await _disciplineService.GetDisciplineByIdAsync(disciplineId);
            var model = new DisciplineViewModel
            {
                Id = discipline.Id,
                Name = discipline.Name,
                SportId = discipline.SportId

            };
            return View(model);
        }

        //POST: /Admin/Sport/{sportId}/Discipline/Edit/{disciplineId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Sport/{sportId}/Discipline/Edit/{disciplineId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int sportId, int disciplineId, DisciplineViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _disciplineService.GetDisciplineByIdAsync(model.Id);
                result.Name = model.Name;
                await _disciplineService.EditAsync(result);
                return RedirectToAction("Sport", "Admin", new { sportId });
            }

            return View(model);
        }
        //GET: /Admin/Sport/{sportId}/Discipline/Details/{disciplineId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Sport/{sportId}/Discipline/Details/{disciplineId}")]
        public async Task<IActionResult> Details(int disciplineId, int sportId)
        {
            var discipline = await _disciplineService.GetDisciplineByIdAsync(disciplineId);
            if (discipline == null)
            {
                return NotFound();
            }
            return View(discipline);
        }

        //Get: /Admin/Sport/{sportId}/Discipline/Delete/{disciplineId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Sport/{sportId}/Discipline/Delete/{disciplineId}")]
        public async Task<IActionResult> Delete(int sportId, int disciplineId)
        {
            var discipline = await _disciplineService.GetDisciplineByIdAsync(disciplineId);
            return View(discipline);
        }

        //Post: /Admin/Sport/{sportId}/Discipline/Delete/{disciplineId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Sport/{sportId}/Discipline/Delete/{disciplineId}")]
        public async Task<IActionResult> DeleteConfirmed(int sportId, int disciplineId)
        {
            await _disciplineService.RemoveAsync(disciplineId);
            return RedirectToAction("Sport", "Admin", new { @sportId = sportId});
        }
    }
}