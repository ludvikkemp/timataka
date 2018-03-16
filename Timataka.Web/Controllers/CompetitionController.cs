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
    public class CompetitionController : Controller
    {
        private readonly ICompetitionService _competitionService;

        public CompetitionController(ICompetitionService competitionService)
        {
            _competitionService = competitionService;
        }

        // Get: Competitions
        public IActionResult Index()
        {
            var competitions = _competitionService.GetAllCompetitions();
            return View(competitions);
        }

        // Get: Competitions/Details/3
        public async Task<IActionResult> Details(int Id)
        {
            var c = await _competitionService.GetCompetitionById(Id);

            if (c == null)
            {
                return NotFound();
            }
            return View(c);
        }

        // Get: Competitions/Create
        public IActionResult Create()
        {
            return View();
        }

        //Post: Competitions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompetitionsViewModel c)
        {
            if (ModelState.IsValid && c.Name != null)
            {
                try
                {
                    await _competitionService.Add(c);
                }
                catch (Exception e)
                {
                    //Todo: return some error view
                }
                return RedirectToAction("Competitions","Admin");
            }
            return View(c);
        }

        // Get: Competitions/Edit/3
        public async Task<IActionResult> Edit(int Id)
        {
            var c = await _competitionService.GetCompetitionById(Id);
            if (c == null)
            {
                return NotFound();
            }
            return View(c);
        }

        // Post: Competitons/Edit/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("Id,Name")] Competition c)
        {
            if (Id != c.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _competitionService.Edit(c);
                return RedirectToAction(nameof(Index));
            }
            return View(c);
        }

        // GET: Competitons/Delete/5
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var c = await _competitionService.GetCompetitionById((int)Id);
            if (c == null)
            {
                return NotFound();
            }

            return View(c);
        }

        // POST: Competitons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var c = await _competitionService.Remove((int)id);
            return RedirectToAction(nameof(Index));
        }

        //ManagesCompetition


        // Get Competitions/ManagesCompetitions
        [HttpGet("/Competitions/ManagesCompetitions/{CompetitionId}")]
        public IActionResult GetRoles(int CompetitionId)
        {
            if(CompetitionId == 0)
            {
                return View(_competitionService.GetAllRoles());
            }
            return View(_competitionService.GetAllRolesForCompetition(CompetitionId));
        }

        // Get Competitons/ManagesCompetitions/Add
        [HttpGet("/Competitons/ManagesCompetitions/Add")]
        public IActionResult AddRole(string UserId, int CompetitionId)
        {
            var m = new {  UserId,  CompetitionId };
            return View(m);
        }

        // Post Competitons/ManagesCompetitions/Add
        [HttpPost("/Competitions/ManagesCompetitions/Add")]
        public IActionResult AddRole(ManagesCompetition m)
        {
            if(ModelState.IsValid)
            {
                _competitionService.AddRole(m);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        

            
    }
}