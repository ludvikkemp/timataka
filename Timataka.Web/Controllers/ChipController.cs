using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.ChipViewModels;
using Timataka.Core.Models.ViewModels.ClubViewModels;
using Timataka.Core.Services;

namespace Timataka.Web.Controllers
{
    public class ChipController : Controller
    {
        private readonly IChipService _chipService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChipController(
            IChipService chipService, 
            UserManager<ApplicationUser> userManager)
        {
            _chipService = chipService;
            _userManager = userManager;
        }

        //GET: /Admin/Chip/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Chip/Create")]
        public IActionResult Create()
        {
            return View();
        }

        //POST: /Admin/Chip/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Chip/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateChipViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User); 
                var codeExists = await _chipService.GetChipByCodeAsync(model.Code);
                if (codeExists == null)
                {
                    var chip = new Chip
                    {
                        Active = model.Active,
                        Code = model.Code,
                        LastCompetitionInstanceId = null,
                        LastSeen = DateTime.Now,
                        Number = model.Number,
                        LastUserId = user.Id
                    };
                    await _chipService.AddChipAsync(chip);
                    return RedirectToAction("Chips", "Admin");
                }

                return Json("Code Already Exists");
            }
            return View(model);
        }

        //GET: /Admin/Chip/Edit/{chipId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Chip/Edit/{chipId}")]
        public IActionResult Edit(int chipId)
        {
            //TODO

            return View();
        }

        //POST: /Admin/Chip/Edit/{chipId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Chip/Edit/{chipId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int chipId, ChipViewModel model)
        {
            //TODO

            return View(model);
        }

        //GET: /Admin/Chip/Details{chipId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Chip/Details/{chipId}")]
        public IActionResult Details(int chipId)
        {
            //TODO

            return View();
        }

        //GET: /Admin/Chip/Delete{chipId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Chip/Delete/{chipId}")]
        public IActionResult Delete(int? chipId)
        {
            //TODO

            return View();
        }

        //POST: /Admin/Chip/Delete/{chipId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Chip/Delete/{chipId}")]
        public async Task<IActionResult> Delete(int id)
        {
            //TODO

            return RedirectToAction("Chips", "Admin");
        }

    }
}
