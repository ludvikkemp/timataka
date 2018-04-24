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
        public IActionResult Create(string code = null)
        {
            CreateChipViewModel model = new CreateChipViewModel { Code = code, Active = false };
            return View(model);
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
                var code = await _chipService.GetChipByCodeAsync(model.Code);
                var number = await _chipService.GetChipByNumberAsync(model.Number);
                if (code == null && number == null)
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
                    return RedirectToAction("ScanChips", "Chip");
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
            var model = new EditChipViewModel
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
        public async Task<IActionResult> Edit(string chipCode, EditChipViewModel model)
        {
            if (ModelState.IsValid)
            {
                var chip = await _chipService.GetChipByCodeAsync(chipCode);
                var number = await _chipService.GetChipByNumberAsync(model.Number);
                if (number != null)
                {
                    return Json("Chip with this number already exists");
                }
                chip.Number = model.Number;
                chip.Active = model.Active;
                var success = await _chipService.EditChipAsync(chip);
                if (success)
                {
                    return RedirectToAction("Chips", "Admin");
                }
            }
            return View(model);
        }

        //GET: /Admin/Chip/ScanChips/
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Chip/ScanChips")]
        public IActionResult ScanChips()
        {
            return View();
        }

        //GET: /Admin/Chip/ScanChips/
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Chip/ScanChips")]
        public async Task<IActionResult> ScanChips(ScanChipViewModel model)
        {
            if (ModelState.IsValid)
            {
                var chip = await _chipService.GetChipByCodeAsync(model.Code);
                if (chip == null) return RedirectToAction("Create", "Chip", new { model.Code });

                chip.LastUserId = null;
                chip.LastCompetitionInstanceId = null;
                var success = await _chipService.EditChipAsync(chip);

                if (success) return RedirectToAction("ScanChips","Chip");
            }

            return View(model);
        }
    }
}
