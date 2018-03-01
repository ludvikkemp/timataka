using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        //GET: Dicipline
        public IActionResult Index()
        {
            var disciplines = _disciplineService.GetAllDisciplines();
            return View(disciplines);
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
                //var sportId = int.Parse(model.SportId);
                var disciplineId = _disciplineService.GetNextId();
                var sportId = int.Parse(model.SportId);
                //var sport = _sportService.GetSportById(sportId);

                var newDiscipline = new Discipline
                {
                    Id = disciplineId,
                    Name = model.Name,
                    SportId = sportId  
                };
                
                await _disciplineService.Add(newDiscipline);

            }
            return RedirectToAction(nameof(Index));
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
            if (discipline == null)
            {
                return NotFound();
            }
            return View(discipline);
        }

        //TODO
        //POST: Dicipline/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] Discipline discipline)
        {
            if (id != discipline.Id)
            {
                return NotFound();
            }

            //TODO

            return View(discipline);
        }

        //TODO
        public IActionResult Delete()
        {
            throw new NotImplementedException();
        }
    }
}