using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // Get: Competitions/Details/3
        public async Task<IActionResult> Details(int id)
        {
            var c = await _competitionService.GetCompetitionById(id);

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

        // Get: Competitions/Edit/3
        public IActionResult Edit(int id)
        {
            var model = _competitionService.GetCompetitionViewModelById(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // Post: Competitons/Edit/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompetitionsViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var instance = await _competitionService.GetCompetitionById(model.Id);
                await _competitionService.Edit(instance, model);
                return RedirectToAction("Competitions", "Admin");
            }
            return View(model);
        }

        // GET: Competitons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var c = await _competitionService.GetCompetitionById((int)id);
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
            var sport = await _competitionService.Remove((int)id);
            return RedirectToAction("Competitions", "Admin");
        }

        //ManagesCompetition
        
        
        // Get Competitions/ManagesCompetitions
 /*       [HttpGet("/Competitions/ManagesCompetitions/{CompetitionId}")]
        public IActionResult GetRoles(int competitionId)
        {
            if(competitionId == 0)
            {
                return View(_competitionService.GetAllRoles());
            }
            return View(_competitionService.GetAllRolesForCompetition(competitionId));
        }
*/
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
                return RedirectToAction("Competitions", "Admin");
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
                return RedirectToAction("Personnel", "Admin", new { @id = model.CompetitionId });
            }
            return View(model);
        }

        // GET: CompetitonInstances/Delete/5
        public async Task<IActionResult> DeleteManager(int? id)
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
        [HttpPost, ActionName("DeleteManager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedManager(int id)
        {
            var instance = _competitionService.GetCompetitionInstanceById(id);
            var compId = instance.Result.CompetitionId;
            await _competitionService.RemoveInstance((int)id);
            return RedirectToAction("Competition", "Admin", new { @id = compId });

        }
    }
}