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
                var exists = await _competitionService.CompetitionExistsAsync(model.Name);
                if (!exists)
                {
                    await _competitionService.AddAsync(model);
                    return RedirectToAction("Competitions","Admin");
                }
                return Json("Competition with this name already exists");
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
                var competition = await _competitionService.GetCompetitionByIdAsync(model.Id);
                await _competitionService.EditAsync(competition, model);
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
            var c = await _competitionService.GetCompetitionByIdAsync(competitionId);

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

            var c = await _competitionService.GetCompetitionByIdAsync((int)competitionId);
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
            var sport = await _competitionService.RemoveAsync((int)competitionId);
            return RedirectToAction("Competitions", "Admin");
        }


        // *** ManagesCompetition *** //
        

        // Get Competitons/ManagesCompetitions/Add
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/Personnel/{userId}/AddRole")]
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
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/Personnel/{userId}/AddRole")]
        [ValidateAntiForgeryToken]
        public IActionResult AddRole(ManagesCompetition m, int competitionId, string userId)
        {
            if(ModelState.IsValid)
            {
                var task = _competitionService.AddRole(m);
                task.Wait();
                return RedirectToAction("Personnel", "Admin", new { competitionId = m.CompetitionId });
            }
            return View();
        }

        // Get: /Admin/Competition/{competitionId}/Personnel/{userId}/EditManager
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/Personnel/{userId}/EditManager")]
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

        // Post: /Admin/Competition/{competitionId}/Personnel/{userId}/EditManager
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/Personnel/{userId}/EditManager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditManager(ManagesCompetitionViewModel model, int competitionId, string userId)
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
                return RedirectToAction("Personnel", "Admin", new { competitionId = model.CompetitionId });
            }
            return View(model);
        }

        // GET: /Admin/Competition/{competitionId}/Personnel/{userId}/DeleteManager
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/Personnel/{userId}/DeleteManager")]
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

        // POST: /Admin/Competition/{competitionId}/Personnel/{userId}/DeleteManager
        [HttpPost, ActionName("DeleteManager")]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/Personnel/{userId}/DeleteManager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedManager(ManagesCompetitionViewModel model, int competitionId, string userId)
        {
            var entity = new ManagesCompetition
            {
                CompetitionId = model.CompetitionId,
                UserId = model.UserId,
                Role = model.Role
            };

            await _competitionService.RemoveRole(entity);
            return RedirectToAction("Personnel", "Admin", new { competitionId = model.CompetitionId });

        }
    }
}