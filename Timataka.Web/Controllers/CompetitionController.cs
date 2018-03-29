using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        //GET: /Admin/Competition/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/Create")]
        public IActionResult Create()
        {
            return View();
        }

        //Post: Admin/Competitions/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompetitionsViewModel model)
        {
            if (ModelState.IsValid && model.Name != null)
            {
                var exists = await _competitionService.CompetitionExists(model.Name);
                if (!exists)
                {
                    await _competitionService.Add(model);
                    return RedirectToAction("Competitions","Admin");
                }
            }
            return View(model);
        }

        //GET: /Admin/Competition/Edit/{clubId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/Edit/{competitionId}")]
        public IActionResult Edit(int competitionId)
        {
            var model = _competitionService.GetCompetitionViewModelById(competitionId);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        //POST: /Admin/Competition/Edit/{clubId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/Edit/{competitionId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int competitionId, CompetitionsViewModel model)
        {
            if (competitionId != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var competition = await _competitionService.GetCompetitionById(model.Id);
                await _competitionService.Edit(competition, model);
                return RedirectToAction("Competitions", "Admin");
            }
            return View(model);
        }

        //GET: /Admin/Competition/Details{competitionId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/Details/{competitionId}")]
        public async Task<IActionResult> Details(int competitionId)
        {
            var c = await _competitionService.GetCompetitionById(competitionId);

            if (c == null)
            {
                return NotFound();
            }
            return View(c);
        }

        //GET: /Admin/Competition/Delete{competitionId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/Delete/{competitionId}")]
        public async Task<IActionResult> Delete(int? competitionId)
        {
            if (competitionId == null)
            {
                return NotFound();
            }

            var c = await _competitionService.GetCompetitionById((int)competitionId);
            if (c == null)
            {
                return NotFound();
            }

            return View(c);
        }

        //POST: /Admin/Competition/Delete/{competitionId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/Delete/{competitionId}")]
        public async Task<IActionResult> Delete(int competitionId)
        {
            var sport = await _competitionService.Remove((int)competitionId);
            return RedirectToAction("Competitions", "Admin");
        }


        // *** ManagesCompetition *** //
        

        // Get Competitons/ManagesCompetitions/Add
        [HttpGet]
        public IActionResult AddRole(string userId, int competitionId)
        {
            ViewBag.userId = userId;
            ViewBag.competitionId = competitionId;

            List<SelectListItem> selectRolesListItems = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Host",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "Official",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Staff",
                    Value = "2"
                }
            };

            ViewBag.roles = selectRolesListItems;

            return View();
        }

        // Post Competitions/ManagesCompetitions/Add
        [HttpPost]
        public IActionResult AddRole(ManagesCompetition m)
        {
            if(ModelState.IsValid)
            {
                var task = _competitionService.AddRole(m);
                task.Wait();
                return RedirectToAction("Personnel", "Admin", new { eventId= m.CompetitionId });
            }
            return View();
        }

        // Get: Competitions/ManagesCompetitions/Edit
        public IActionResult EditManager(string userId, int competitionId)
        {
            var modelAllCompetitionRoles = _competitionService.GetAllRolesForCompetition(competitionId);

            var model = new ManagesCompetitionViewModel();

            foreach (var item in modelAllCompetitionRoles)
            {
                if(item.UserId == userId)
                {
                    model = item;
                }
            }

            List<SelectListItem> selectRolesListItems = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Host",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "Official",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Staff",
                    Value = "2"
                }
            };

            ViewBag.roles = selectRolesListItems;

            return View(model);
        }

        // Post: Competitions/ManagesCompetitions/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditManager(ManagesCompetitionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new ManagesCompetition
                {
                    CompetitionId = model.CompetitionId,
                    UserId = model.UserId,
                    Role = model.Role
                };
                await _competitionService.EditRole(entity);
                return RedirectToAction("Personnel", "Admin", new { eventId = model.CompetitionId });
            }
            return View(model);
        }

        // GET: Competition/ManagesCompetition/Delete
        public IActionResult DeleteManager(string userId, int competitionId)
        {
            var modelAllCompetitionRoles = _competitionService.GetAllRolesForCompetition(competitionId);

            var model = new ManagesCompetitionViewModel();

            foreach (var item in modelAllCompetitionRoles)
            {
                if (item.UserId == userId)
                {
                    model = item;
                }
            }

            return View(model);
        }

        // POST: Competition/ManagesCompetition/Delete
        [HttpPost, ActionName("DeleteManager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedManager(ManagesCompetitionViewModel model)
        {
            var entity = new ManagesCompetition
            {
                CompetitionId = model.CompetitionId,
                UserId = model.UserId,
                Role = model.Role
            };

            await _competitionService.RemoveRole(entity);
            return RedirectToAction("Personnel", "Admin", new { eventId = model.CompetitionId });

        }
    }
}