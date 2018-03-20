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

    }
}