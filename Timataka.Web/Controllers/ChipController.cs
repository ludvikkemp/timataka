using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.ChipViewModels;
using Timataka.Core.Services;

namespace Timataka.Web.Controllers
{
    public class ChipController : Controller
    {
        private readonly IChipService _chipService;

        public ChipController(
            IChipService chipService)
        {
            _chipService = chipService;
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
                        LastUserId = null
                    };
                    await _chipService.AddChipAsync(chip);
                    return RedirectToAction("Chips", "Admin");
                }
                return Json("Code Already Exists");
            }
            return View(model);
        }

        //GET: /Admin/Chip/Edit/{chipCode}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Chip/Edit/{chipCode}")]
        public async Task<IActionResult> Edit(string chipCode)
        {
            var chip = await _chipService.GetChipByCodeAsync(chipCode);
            if (chip == null)
            {
                return NotFound();
            }
            var model = new CreateChipViewModel
            {
                Code = chip.Code,
                Number = chip.Number,
                Active = chip.Active
            };
            return View(model);
        }

        //POST: /Admin/Chip/Edit/{chipCode}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Chip/Edit/{chipCode}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string chipCode, CreateChipViewModel model)
        {
            //TODO

            return View(model);
        }

        //GET: /Admin/Chip/Details{chipCode}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Chip/Details/{chipCode}")]
        public IActionResult Details(string chipCode)
        {
            //TODO

            return View();
        }

        //GET: /Admin/Chip/Delete{chipCode}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Chip/Delete/{chipCode}")]
        public IActionResult Delete(string chipCode)
        {
            //TODO

            return View();
        }

        //POST: /Admin/Chip/Delete/{chipCode}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Chip/Delete/{chipCode}")]
        public async Task<IActionResult> Delete(string chipCode, CreateChipViewModel model)
        {
            //TODO

            return RedirectToAction("Chips", "Admin");
        }

    }
}
