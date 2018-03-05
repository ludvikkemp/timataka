using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Services;

namespace Timataka.Web.Controllers
{
    public class CompetitionController : Controller
    {
        private readonly ICompetitionService _competitionService;

        public CompetitionController(ICompetitionService competitionService)
        {
            _competitionService = competitionService;
        }

        // Get: Competitions
        public IActionResult Index()
        {
            var competitions = _competitionService.GetAllCompetitions();
            return View(competitions);
        }
    }
}