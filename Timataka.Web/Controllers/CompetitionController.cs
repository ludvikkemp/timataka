using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.Entities;
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
        public async Task<IActionResult> Add([Bind("Id,Name")] Competition c)
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
                return RedirectToAction(nameof(Index));
            }
            return View(c);
        }

        // Get: Competitions/Edit/3
        public IActionResult Edit(int Id)
        {
            var c = _competitionService.GetCompetitionById(Id);
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

        //ManagesCompetition
        // Get Competitions/ManagesCompetition
        [HttpGet]
        public IActionResult GetRoles(int? CompetitionId)
        {
            var m = 1;
            if(CompetitionId == null)
            {
                return View(m);
            }
            return View(m);
        }
            
    }
}