using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;
using Timataka.Core.Services;

namespace Timataka.Web.Controllers
{
    public class CompetitionInstanceController : Controller
    {
        private readonly ICompetitionService _competitionService;
        private readonly IAccountService _accountService;

        public CompetitionInstanceController(ICompetitionService competitionService, 
                                             IAccountService accountService)
        {
            _competitionService = competitionService;
            _accountService = accountService;
        }

        // Get: CompetitionInstances/Details/3
        public async Task<IActionResult> Details(int id)
        {
            var c = await _competitionService.GetCompetitionInstanceById(id);

            if (c == null)
            {
                return NotFound();
            }
            return View(c);
        }

        // Get: CompetitionInstances/Create
        public IActionResult Create()
        {
            ViewBag.CompetitionIds = _competitionService.GetAllCompetitions();
            ViewBag.Nations = _accountService.GetNationsListItems();
            return View();
        }

        // Post: CompetitionInstances/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompetitionsInstanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _competitionService.AddInstance(model);
                }
                catch (Exception e)
                {
                    //Todo: return some error view
                }
                return RedirectToAction("Competitions","Admin");
            }
            return View(model);
        }

        // Get: CompetitionInstances/Edit/3
        public IActionResult Edit(int id)
        {
            var model = _competitionService.GetCompetitionInstanceViewModelById(id);
            ViewBag.ListOfNations = _accountService.GetNationsListItems();
            return View(model);
        }

        // Post: CompetitonInstances/Edit/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompetitionsInstanceViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var compInstance = await _competitionService.GetCompetitionInstanceById(model.Id);
                await _competitionService.EditInstance(compInstance, model);
                return RedirectToAction("Competition", "Admin", new { @id = compInstance.CompetitionId });
            }
            return View(model);
        }

        // GET: CompetitonInstances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var c = await _competitionService.GetCompetitionInstanceById((int)id);
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
            var instance = _competitionService.GetCompetitionInstanceById(id);
            var compId = instance.Result.CompetitionId;
            await _competitionService.RemoveInstance((int)id);
            return RedirectToAction("Competition", "Admin", compId);

        }


    }
}