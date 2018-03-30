using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.AdminViewModels;
using Timataka.Core.Services;

namespace Timataka.Web.Controllers
{
    public class SportController : Controller
    {
        private readonly ISportService _sportService;

        public SportController(ISportService sportService)
        {
            _sportService = sportService;
        }

        // GET: /Admin/Sport/Details/{sportId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Sport/Details/{sportId}")]
        public async Task<IActionResult> Details(int sportId)
        {
            var sport = await _sportService.GetSportByIdAsync(sportId);

            if (sport == null)
            {
                return NotFound();
            }
            return View(sport);
        }

        // GET: /Admin/Sports/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Sport/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Sports/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Sport/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SportsViewModel sport)
        {
            if (ModelState.IsValid)
            {
                var newSport = await _sportService.Add(sport);
                return RedirectToAction("Sport", "Admin", new { @sportId = newSport.Id });
            }
            return View(sport);
        }

        // GET: /Admin/Sport/Edit/{sportId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Sport/Edit/{sportId}")]
        public IActionResult Edit(int sportId)
        {
            var sport = _sportService.GetSportByIdAsync(sportId);
            if (sport == null)
            {
                return NotFound();
            }
            return View(sport.Result);
        }

        // POST: /Admin/Sports/Edit/{sportId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Sport/Edit/{sportId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int sportId, Sport sport)
        {
            if (ModelState.IsValid)
            {
                await _sportService.Edit(sport);
                return RedirectToAction("Sports", "Admin");
            }
            return View(sport);
        }

        // GET: /Admin/Sports/Delete/{sportId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Sport/Delete/{sportId}")]
        public async Task<IActionResult> Delete(int sportId)
        {
            var sport = await _sportService.GetSportByIdAsync(sportId);
            return View(sport);
        }

        // POST: /Admin/Sports/Delete/{sportId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Sport/Delete/{sportId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int sportId)
        {
            await _sportService.Remove(sportId);
            return RedirectToAction("Sports","Admin");
        }
    }
}
