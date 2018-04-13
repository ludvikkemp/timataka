using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.ViewModels.ChipViewModels;
using Timataka.Core.Models.ViewModels.ClubViewModels;
using Timataka.Core.Services;

namespace Timataka.Web.Controllers
{
    public class ChipController : Controller
    {
        private readonly IChipService _chipService;

        public ChipController(IChipService chipService)
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
        public async Task<IActionResult> Create(ChipViewModel model)
        {
            //TODO

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
