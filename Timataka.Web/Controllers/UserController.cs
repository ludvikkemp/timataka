using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.Entities;
using Timataka.Core.Services;
using Timataka.Core.Models.Dto.UserDTO;
using Microsoft.AspNetCore.Authorization;
using Timataka.Core.Models.Dto.AdminDTO;

namespace Timataka.Web.Controllers
{
    public class UserController : Controller
    {

        private readonly ICompetitionService _competitionService;
        private readonly IEventService _eventService;
        private readonly IHeatService _heatService;
        private readonly IResultService _resultService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(ICompetitionService competitionService,
                              IEventService eventService, 
                              IHeatService heatService,
                              IResultService resultService,
                              UserManager<ApplicationUser> userManager)
        {
            _competitionService = competitionService;
            _eventService = eventService;
            _heatService = heatService;
            _userManager = userManager;
            _resultService = resultService;
        }

        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("User/Competitions")]
        [Authorize(Roles = "User")]
        public IActionResult Competitions(string search)
        {
            ViewData["CurrentFilter"] = search;
            var instances = _competitionService.GetAllCompetitionInstances() ;

            if(!String.IsNullOrEmpty(search))
            {
                instances = instances.Where(x => x.Name.ToUpper().Contains(search.ToUpper()));
            }

            instances = instances.OrderByDescending(x => x.DateFrom);

            return View(instances);
        }

        [HttpGet]
        [Route("User/Competitions/{competitionInstanceId}/Events")]
        [Authorize(Roles = "User")]
        public IActionResult Events(int competitionInstanceId)
        {
            var events = _eventService.GetEventsByCompetitionInstanceId(competitionInstanceId);
            ViewBag.userId = _userManager.GetUserAsync(User).Result.Id;

            events = events.OrderByDescending(x => x.DateFrom);
            return View(events);
        }

        [HttpGet]
        [Route("User/Competitions/Events/{eventId}/RegisterToEvent/{userId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RegisterToEvent(string userId, int eventId)
        {
            var eventObj = await _eventService.GetEventByIdAsync(eventId);
            var instance = await _competitionService.GetCompetitionInstanceByIdAsync(eventObj.CompetitionInstanceId);
            
            var model = new RegisterToEventDto
            {
                Event = eventObj,
                Instance = instance,
                UserId = userId
            };

            return View(model);
        }

        [HttpPost]
        [Route("User/Competitions/Events/{eventId}/RegisterToEvent/{userId}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RegisterToEvent(RegisterToEventDto model, string userId, int eventId)
        {
            if (ModelState.IsValid)
            {
                var heatsInEvent = _heatService.GetHeatsForEvent(model.Event.Id);

                var heatId = (from x in heatsInEvent
                              where x.HeatNumber == 0
                              select x.Id).SingleOrDefault();

                var contestantInHeat = new ContestantInHeat
                {
                    HeatId = heatId,
                    UserId = model.UserId,
                    Modified = DateTime.Now
                };

                await _heatService.AddAsyncContestantInHeat(contestantInHeat);

                return RedirectToAction("Events", "User", new { @competitionInstanceId = model.Instance.Id });
            }

            return View(model);
        }

        [HttpGet]
        [Route("User/MyCompetitions")]
        [Authorize(Roles = "User")]
        public IActionResult MyCompetitions(string search)
        {
            ViewData["CurrentFilter"] = search;

            var userId = _userManager.GetUserAsync(User).Result.Id;

            var instances = _competitionService.GetAllCompetitionInstancesForUser(userId);

            if (!String.IsNullOrEmpty(search))
            {
                instances = instances.Where(x => x.Instance.Name.ToUpper().Contains(search.ToUpper()));
            }
             
            instances = instances.OrderByDescending(x => x.Instance.DateFrom);

            return View(instances);
        }

        [HttpGet]
        [Route("User/MyCompetitions/{eventId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> MyEvent(int eventId)
        {
            var eventObj = await _eventService.GetEventByIdAsync(eventId);
            var instance = await _competitionService.GetCompetitionInstanceByIdAsync(eventObj.CompetitionInstanceId);
            var competition = await _competitionService.GetCompetitionByIdAsync(instance.CompetitionId);
            

            var model = new EventDto
            {
                Competition = competition,
                CompetitionInstance = instance,
                Event = eventObj,
                Heats = null
            };

            return View(model);
        }

        [HttpGet]
        [Route("User/Results")]
        [Authorize(Roles = "User")]
        public IActionResult Results(string search)
        {
            ViewData["CurrentFilter"] = search;
            var instances = _competitionService.GetAllCompetitionInstances();

            if (!String.IsNullOrEmpty(search))
            {
                var searchToUpper = search.ToUpper();
                instances = instances.Where(u => u.Name.ToUpper().Contains(searchToUpper));
            }

            instances = instances.OrderByDescending(x => x.DateFrom);

            return View(instances);
        }

        [HttpGet]
        [Route("User/Results/{competitionInstanceId}/Event")]
        [Authorize(Roles = "User")]
        public IActionResult EventsResults(string search, int competitionInstanceId)
        {
            ViewData["CurrentFilter"] = search;
            var events = _eventService.GetEventsByCompetitionInstanceId(competitionInstanceId);

            if (!String.IsNullOrEmpty(search))
            {
                var searchToUpper = search.ToUpper();
                events = events.Where(u => u.Name.ToUpper().Contains(searchToUpper));
            }

            return View(events);
        }

        [HttpGet]
        [Route("User/Results/{competitionInstanceId}/Event/{eventId}")]
        [Authorize(Roles = "User")]
        public IActionResult Result(int eventId, int competitionInstanceId)
        {
            var model = _resultService.GetResultViewModelsForEvent(eventId);
            return View(model);
        }

        [HttpGet]
        [Route("User/MyResults")]
        [Authorize(Roles = "User")]
        public IActionResult MyResults(string search)
        {
            var userId = _userManager.GetUserAsync(User).Result.Id;

            var model = _resultService.GetResultsForUser(userId);

            model = model.OrderBy(x => x.GunTime);
            model = model.OrderBy(x => x.DisciplineName);

            return View(model);
        }
    }
}