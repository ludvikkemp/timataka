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

        public DisciplineController(IDisciplineService disciplineService)
        {
            _disciplineService = disciplineService;
        }

        //GET: Dicipline/Create
        public IActionResult Create()
        {
            ViewBag.Sports = _disciplineService.GetSportsListItems();
            return View();
        }

        //POST: Dicipline/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid && model.Name != null)
            {
                var sportId = int.Parse(model.SportId);

                var newDiscipline = new Discipline
                {
                    Name = model.Name,
                    SportId = sportId  
                };
                await _disciplineService.Add(newDiscipline);
            }
            return RedirectToAction("Sport","Admin", new {@id = model.SportId});
        }

        //GET: Dicipline/Details/1
        public IActionResult Details(int id)
        {
            var discipline = _disciplineService.GetDisciplineById(id);
            if (discipline == null)
            {
                return NotFound();
            }
            return View(discipline);
        }

        //GET: Dicipline/Edit/1
        public IActionResult Edit(int id)
        {
            var discipline = _disciplineService.GetDisciplineById(id);
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

        //TODO
        //POST: Dicipline/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DisciplineViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var res = _disciplineService.GetDisciplineById(model.Id);
                res.Name = model.Name;
                await _disciplineService.Edit(res);
                return RedirectToAction("Sport", "Admin", new {@id = model.SportId});
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete()
        {
            return View();
        }
    }
}