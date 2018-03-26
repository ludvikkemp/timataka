using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.ViewModels.HeatViewModels;
using Timataka.Core.Services;
using Timataka.Core.Models.Entities;

namespace Timataka.Web.Controllers
{
    public class HeatController : Controller
    {
        private readonly IHeatService _heatService;

        public HeatController(IHeatService heatService)
        {
            _heatService = heatService;
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


        public IActionResult AddContestant(int heatId, string userId)
        {
            return View();
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