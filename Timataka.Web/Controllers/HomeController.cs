using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models;
using Timataka.Core.Models.ViewModels;
using Timataka.Core.Services;
using Timataka.Core.Models.Dto.AdminDTO;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;
using Timataka.Core.Models.ViewModels.HeatViewModels;
using Timataka.Core.Models.ViewModels.HomeViewModels;
using Timataka.Core.Models.Dto.CompetitionInstanceDTO;
using Timataka.Core.Models.Dto.ResultsDTO;

namespace Timataka.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICompetitionService _competitionService;
        private readonly IEventService _eventService;
        private readonly IHeatService _heatService;
        private readonly IResultService _resultService;
        private readonly ICategoryService _categoryService;
        private const int AthleticsId = 1;
        private const int CyclingId = 2;
        private const int OTHER = 0;


        public HomeController(
            ICompetitionService competitionService,
            IEventService eventService,
            IHeatService heatService,
            IResultService resultService,
            ICategoryService categoryService)
        {
            _competitionService = competitionService;
            _eventService = eventService;
            _heatService = heatService;
            _resultService = resultService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var model = new HomePageViewModel
            {
                LatestAthleticsResults = _competitionService.GetLatestResults(AthleticsId, true),
                UpcomingAthleticsEvents = _competitionService.GetUpcomingEvents(AthleticsId, true),
                LatestCyclingResults = _competitionService.GetLatestResults(CyclingId, true),
                UpcomingCyclingEvents = _competitionService.GetUpcomingEvents(CyclingId, true),
                LatestOtherResults = _competitionService.GetLatestResults(0, true),
                UpcomingOtherEvents = _competitionService.GetUpcomingEvents(0, true),
                LatestResults = _competitionService.GetLatestResults(null, true),
                UpcomingEvents = _competitionService.GetUpcomingEvents(null, true)
            };

            return View(model);
        }

        [HttpGet]
        [Route("Results")]
        public IActionResult Results(string search)
        {
            ViewData["CurrentFilter"] = search;
            var all = _competitionService.GetLatestResults(null,false);
            var athletics = _competitionService.GetLatestResults(AthleticsId, false);
            var cycling = _competitionService.GetLatestResults(CyclingId, false);
            var other = _competitionService.GetLatestResults(0, false);

            if (!String.IsNullOrEmpty(search))
            {
                var searchToUpper = search.ToUpper();
                all = all.Where(u => u.CompetitionInstanceName.ToUpper().Contains(searchToUpper));
                athletics = athletics.Where(u => u.CompetitionInstanceName.ToUpper().Contains(searchToUpper));
                cycling = cycling.Where(u => u.CompetitionInstanceName.ToUpper().Contains(searchToUpper));
                other = other.Where(u => u.CompetitionInstanceName.ToUpper().Contains(searchToUpper));
            }
            var model = new ResultsDTO { All = all, Athletics = athletics, Cycling = cycling, Other = other };

            return View(model);
        }

        [HttpGet]
        [Route("Upcoming")]
        public IActionResult Upcoming(string search)
        {
            ViewData["CurrentFilter"] = search;
            var all = _competitionService.GetUpcomingEvents(null, false);
            var athletics = _competitionService.GetUpcomingEvents(AthleticsId, false);
            var cycling = _competitionService.GetUpcomingEvents(CyclingId, false);
            var other = _competitionService.GetUpcomingEvents(0, false);

            if (!String.IsNullOrEmpty(search))
            {
                var searchToUpper = search.ToUpper();
                all = all.Where(u => u.CompetitionInstanceName.ToUpper().Contains(searchToUpper));
                athletics = athletics.Where(u => u.CompetitionInstanceName.ToUpper().Contains(searchToUpper));
                cycling = cycling.Where(u => u.CompetitionInstanceName.ToUpper().Contains(searchToUpper));
                other = other.Where(u => u.CompetitionInstanceName.ToUpper().Contains(searchToUpper));
            }
            var model = new ResultsDTO { All = all, Athletics = athletics, Cycling = cycling, Other = other };

            return View(model);
        }

        [HttpGet]
        [Route("Results/Competition/{competitionId}")]
        public IActionResult Competition(string search, int competitionId)
        {
            ViewData["CurrentFilter"] = search;
            var competition = _competitionService.GetCompetitionByIdAsync(competitionId);
            competition.Wait();
            var competitionInstances = _competitionService.GetAllInstancesOfCompetition(competitionId);

            if (!String.IsNullOrEmpty(search))
            {
                var searchToUpper = search.ToUpper();
                competitionInstances = competitionInstances.Where(u => u.Name.ToUpper().Contains(searchToUpper));
            }

            var compDto = new CompetitionDto
            {
                Competiton = competition.Result,
                Instances = competitionInstances
            };

            return View(compDto);
        }

        [HttpGet]
        [Route("Results/Competition/{competitionId}/Instance/{instanceId}")]
        public IActionResult Instance(string search, int competitionId, int instanceId)
        {
            var instanceTask = _competitionService.GetCompetitionInstanceByIdAsync(instanceId);
            instanceTask.Wait();

            var instance = new CompetitionsInstanceViewModel
            {
                Id = instanceTask.Result.Id,
                CompetitionId = instanceTask.Result.CompetitionId,
                DateFrom = instanceTask.Result.DateFrom,
                DateTo = instanceTask.Result.DateTo,
                Location = instanceTask.Result.Location,
                CountryId = instanceTask.Result.CountryId,
                Name = instanceTask.Result.Name,
                Status = instanceTask.Result.Status,
                Deleted = instanceTask.Result.Deleted
            };
            
            var events = _eventService.GetEventsByCompetitionInstanceId(instanceId);

            if (!String.IsNullOrEmpty(search))
            {
                var searchToUpper = search.ToUpper();
                events = events.Where(u => u.Name.ToUpper().Contains(searchToUpper));
            }

            var competition = _competitionService.GetCompetitionByIdAsync(competitionId);
            competition.Wait();

            events = events.OrderByDescending(x => x.DateFrom);

            var instanceDto = new CompetitionInstanceDTO
            {
                Competition = competition.Result,
                CompetitionInstance = instance,
                Events = events
            };

            return View(instanceDto);
        }

        [HttpGet]
        [Route("Results/Competition/{competitionId}/Instance/{instanceId}/Event/{eventId}/Result")]
        public IActionResult Event(int competitionId, int instanceId, int eventId)
        {
            var eventObj = _eventService.GetEventByIdAsync(eventId);
            eventObj.Wait();
            ViewBag.EventName = eventObj.Result.Name;

            var competition = _competitionService.GetCompetitionByIdAsync(competitionId);
            competition.Wait();
            ViewBag.CompetitionName = competition.Result.Name;

            var competitionInstance = _competitionService.GetCompetitionInstanceByIdAsync(instanceId);
            competitionInstance.Wait();
            ViewBag.CompetitionInstanceName = competitionInstance.Result.Name;
            
            var models = _resultService.GetResultViewModelsForEvent(eventId);

            return View(models);
        }

        [HttpGet]
        [Route("Results/Competition/{competitionId}/Instance/{instanceId}/Event/{eventId}/StartList")]
        public IActionResult StartList(int competitionId, int instanceId, int eventId)
        {
            var eventObj = _eventService.GetEventByIdAsync(eventId);
            eventObj.Wait();
            ViewBag.EventName = eventObj.Result.Name;

            var competition = _competitionService.GetCompetitionByIdAsync(competitionId);
            competition.Wait();
            ViewBag.CompetitionName = competition.Result.Name;

            var competitionInstance = _competitionService.GetCompetitionInstanceByIdAsync(instanceId);
            competitionInstance.Wait();
            ViewBag.CompetitionInstanceName = competitionInstance.Result.Name;

            var models = _resultService.GetStartListViewModelsForEvent(eventId);

            return View(models);
        }

        [HttpGet]
        [Route("Results/Competition/{competitionId}/Instance/{instanceId}/Event/{eventId}/Category/{categoryId}/Result")]
        public async Task<IActionResult> Category(int competitionId, int instanceId, int eventId, int categoryId)
        {
            var eventObj = _eventService.GetEventByIdAsync(eventId);
            eventObj.Wait();
            ViewBag.EventName = eventObj.Result.Name;

            var competition = _competitionService.GetCompetitionByIdAsync(competitionId);
            competition.Wait();
            ViewBag.CompetitionName = competition.Result.Name;

            var competitionInstance = _competitionService.GetCompetitionInstanceByIdAsync(instanceId);
            competitionInstance.Wait();
            ViewBag.CompetitionInstanceName = competitionInstance.Result.Name;

            var models = _resultService.GetResultViewModelsForEvent(eventId);

            var category = await _categoryService.GetCategoryViewModelById(categoryId);
            ViewBag.CategoryName = category.Name;

            var model = (from r in models
                         where category.AgeFrom <= (DateTime.Now.Year - r.DateOfBirth.Year) &&
                         category.AgeTo >= (DateTime.Now.Year - r.DateOfBirth.Year) &&
                         (category.Gender.ToString().ToLower() == r.Gender.ToLower() || category.Gender.ToString().ToLower() == "all")
                         select r).ToList();

            if (category.CountryId != null)
            {
                model = (from m in model
                          where category.CountryName.Equals(m.Country)
                          select m).ToList();
            }

            return View(model);
        }

        [HttpGet]
        [Route("Results/Competition/{competitionId}/Instance/{instanceId}/Event/{eventId}/CategoryStartList/{categoryId}/Result")]
        public async Task<IActionResult> CategoryStartList(int competitionId, int instanceId, int eventId, int categoryId)
        {
            var eventObj = _eventService.GetEventByIdAsync(eventId);
            eventObj.Wait();
            ViewBag.EventName = eventObj.Result.Name;

            var competition = _competitionService.GetCompetitionByIdAsync(competitionId);
            competition.Wait();
            ViewBag.CompetitionName = competition.Result.Name;

            var competitionInstance = _competitionService.GetCompetitionInstanceByIdAsync(instanceId);
            competitionInstance.Wait();
            ViewBag.CompetitionInstanceName = competitionInstance.Result.Name;

            var models = _resultService.GetStartListViewModelsForEvent(eventId);

            var category = await _categoryService.GetCategoryViewModelById(categoryId);
            ViewBag.CategoryName = category.Name;

            var model = (from r in models
                         where category.AgeFrom <= (DateTime.Now.Year - r.DateOfBirth.Year) &&
                         category.AgeTo >= (DateTime.Now.Year - r.DateOfBirth.Year) &&
                         (category.Gender.ToString().ToLower() == r.Gender.ToLower() || category.Gender.ToString().ToLower() == "all")
                         select r).ToList();

            if (category.CountryId != null)
            {
                model = (from m in model
                         where category.CountryName.Equals(m.Country)
                         select m).ToList();
            }

            return View(model);
        }

        [HttpGet]
        [Route("Athletics")]
        public IActionResult Athletics()
        {
            var model = new DisplayResultsViewModel
            {
                LatestResults = _competitionService.GetLatestResults(AthleticsId, false),
                UpcomingEvents = _competitionService.GetUpcomingEvents(AthleticsId, false),
            };
            return View(model);
        }

        [HttpGet]
        [Route("Cycling")]
        public IActionResult Cycling()
        {
            var model = new DisplayResultsViewModel
            {
                LatestResults = _competitionService.GetLatestResults(CyclingId, false),
                UpcomingEvents = _competitionService.GetUpcomingEvents(CyclingId, false),
            };
            return View(model);
        }

        [HttpGet]
        [Route("OtherSports")]
        public IActionResult OtherSports()
        {
            var model = new DisplayResultsViewModel
            {
                LatestResults = _competitionService.GetLatestResults(0, false),
                UpcomingEvents = _competitionService.GetUpcomingEvents(0, false),
            };
            return View(model);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
