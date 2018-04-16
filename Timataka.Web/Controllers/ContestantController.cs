using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;
using Timataka.Core.Services;

namespace Timataka.Web.Controllers
{
    public class ContestantController : Controller
    {
        private readonly IHeatService _heatService;

        public ContestantController(IHeatService heatService)
        {
            _heatService = heatService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
    }
}