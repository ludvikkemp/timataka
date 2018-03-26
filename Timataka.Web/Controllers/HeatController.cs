using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.ViewModels.HeatViewModels;
using Timataka.Core.Services;
using Timataka.Core.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Timataka.Web.Controllers
{
    public class HeatController : Controller
    {
        private readonly IHeatService _heatService;
        private readonly IAdminService _adminService;

        public HeatController(IHeatService heatService,
                              IAdminService adminService)
        {
            _heatService = heatService;
            _adminService = adminService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int id)
        {
            ViewBag.EventId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HeatViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _heatService.AddAsync(model.EventId);
                }
                catch (Exception e)
                {
                    //Todo: return some error view
                }
                return RedirectToAction("Event", "Admin", new { @id = model.EventId });
            }
            return View(model);
        }

        // Get: Heat/Edit/3
        public IActionResult Edit(int id)
        {
            var task = _heatService.GetHeatByIdAsync(id);
            var entity = task.Result;
            return View(entity);
        }

        // Post: Heat/Edit/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Heat entity)
        {
            if (id != entity.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _heatService.EditAsync(entity);
                return RedirectToAction("Event", "Admin", new { @id = entity.EventId });
            }
            return View(entity);
        }

        // GET: Heat/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = _heatService.GetHeatByIdAsync((int)id);
            var entity = task.Result;
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // POST: Heat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var heat = _heatService.GetHeatByIdAsync(id);
            var eventId = heat.Result.EventId;
            await _heatService.RemoveAsync(id);
            return RedirectToAction("Event", "Admin", new { @id = eventId });

        }


        //Contestants In Heat

        
        // GET: HeatController/AddContestant
        public IActionResult AddContestant(int heatId)
        {
            List<SelectListItem> selectUsersListItems =
                new List<SelectListItem>();

            var listOfUsers = _adminService.GetUsers();

            foreach (var item in listOfUsers)
            {
                selectUsersListItems.Add(
                    new SelectListItem
                    {
                        Text = item.FirstName + ' ' + item.Middlename + ' ' + item.LastName + " (" + item.Ssn + ")",
                        Value = item.Id
                    });
            }

            ViewBag.heatId = heatId;
            ViewBag.users = selectUsersListItems;
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContestant(ContestantsInHeatViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entitiy = new ContestantInHeat
                    {
                        Bib = model.Bib,
                        HeatId = model.HeatId,
                        Modified = model.Modified,
                        Team = model.Team,
                        UserId = model.UserId
                    };

                    await _heatService.AddAsyncContestantInHeat(entitiy);
                }
                catch(Exception e)
                {
                    return new BadRequestResult();
                }
                return RedirectToAction("Heat", "Admin", new { @id = model.HeatId });
            }
            return View(model);
        }

        public IActionResult EditContestant(int heatId, string userId)
        {
            var entitiy = _heatService.GetContestantInHeatById(heatId,userId);
            
            var model = new ContestantsInHeatViewModel
            {
                Bib = entitiy.Bib,
                HeatId = entitiy.HeatId,
                Modified = entitiy.Modified,
                Team = entitiy.Team,
                UserId = entitiy.UserId
            };

            return View(model);
        } 

        public IActionResult RemoveContestant(int heatId, string userId)
        {
            return View();
        }
    }
}