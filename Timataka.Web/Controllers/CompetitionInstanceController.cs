using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.Entities;
using Timataka.Core.Services;

namespace Timataka.Web.Controllers
{
    public class CompetitionInstanceController : Controller
    {
        private readonly ICompetitionService _competitionService;

        public CompetitionInstanceController(ICompetitionService competitionService)
        {
            _competitionService = competitionService;
        }

        // Get: CompetitionInstances
        public IActionResult Index()
        {
            var Instances = _competitionService.GetAllCompetitionInstances();
            return View(Instances);
        }

        // Get: CompetitionInstances/Details/3
        public async Task<IActionResult> Details(int Id)
        {
            var c = await _competitionService.GetCompetitionInstanceById(Id);

            if (c == null)
            {
                return NotFound();
            }
            return View(c);
        }

        // Get: CompetitionInstances/Create
        public IActionResult Create()
        {
            return View();
        }

        // Post: CompetitionInstances/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CompetitionInstance c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _competitionService.AddInstance(c);
                }
                catch (Exception e)
                {
                    //Todo: return some error view
                }
                return RedirectToAction(nameof(Index));
            }
            return View(c);
        }

        // Get: CompetitionInstances/Edit/3
        public IActionResult Edit(int Id)
        {
            var c = _competitionService.GetCompetitionInstanceById(Id);
            if (c == null)
            {
                return NotFound();
            }
            return View(c);
        }

        // Post: CompetitonInstances/Edit/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, CompetitionInstance c)
        {
            if (Id != c.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _competitionService.EditInstance(c);
                return RedirectToAction(nameof(Index));
            }
            return View(c);
        }

        // GET: CompetitonInstances/Delete/5
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var c = await _competitionService.GetCompetitionInstanceById((int)Id);
            if (c == null)
            {
                return NotFound();
            }

            return View(c);
        }

        // POST: CompetitonInstances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sport = await _competitionService.RemoveInstance((int)id);
            return RedirectToAction(nameof(Index));
        }


    }
}