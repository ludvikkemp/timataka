﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.ViewModels.ClubViewModels;
using Timataka.Core.Services;

namespace Timataka.Web.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubService _clubService;

        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateClubViewModel model)
        {
            if (ModelState.IsValid && model.Name != null)
            {
                var exists = await _clubService.ClubExistsAsync(model.Name);
                if (!exists)
                {
                    await _clubService.AddAsync(model);
                    return RedirectToAction("Clubs", "Admin");
                }
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = _clubService.GetClubViewModelById(id);
            if (model == null)
            {
                return NotFound();
            }
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditClubViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _clubService.EditClubAsync(model);
                return RedirectToAction("Clubs", "Admin");
            }
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var club = _clubService.GetClubViewModelById(id);
            if (club == null)
            {
                return NotFound();
            }
            return View(club);
        }

 
        // GET: Club/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var c = _clubService.GetClubViewModelById((int)id);
            
            if (c == null)
            {
                return NotFound();
            }

            c.Id = (int)id;

            return View(c);
        }

        // POST: Club/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var c = await _clubService.RemoveAsync(id);
            return RedirectToAction("Clubs", "Admin");
        }

    }
}