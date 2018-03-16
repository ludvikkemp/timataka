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
            /*
            var result = _competitionService.GetCompetitionInstanceById(id);
            result.Wait();
            ViewBag.ListOfNations = _accountService.GetNationsListItems();
            
            if (result.Result.CountryId != null)
            {
                var countryId = (int) result.Result.CountryId;
                var model = new CompetitionsInstanceViewModel
                {
                    Id = result.Result.Id,
                    CompetitionId = result.Result.CompetitionId,
                    Name = result.Result.Name,
                    DateFrom = result.Result.DateFrom,
                    DateTo = result.Result.DateTo,
                    Location = result.Result.Location,
                    Status = result.Result.Status,
                    Country = countryId
                };
                return View(model);
            }
            */
            return null;
        }

        // Post: CompetitonInstances/Edit/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompetitionInstance c)
        {
            if (id != c.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _competitionService.EditInstance(c);
                return RedirectToAction("Competition", "Admin", c.CompetitionId);
            }
            return View(c);
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