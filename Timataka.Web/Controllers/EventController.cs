using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Dto.CompetitionInstanceDTO;
using Timataka.Core.Services;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.EventViewModels;
using Timataka.Core.Models.ViewModels.DeviceViewModels;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;

namespace Timataka.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IDisciplineService _disciplineService;
        private readonly ICourseService _courseService;
        private readonly IDeviceService _deviceService;
        private readonly ICompetitionService _competitionService;

        public EventController(IEventService eventService,
            IDisciplineService disciplineService,
            ICourseService courseService,
            IDeviceService deviceService,
            ICompetitionService competitionService)
        {
            _disciplineService = disciplineService;
            _eventService = eventService;
            _courseService = courseService;
            _deviceService = deviceService;
            _competitionService = competitionService;
        }

        #region Event

        //GET: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/Create")]
        public IActionResult Create(int competitionInstanceId, int competitionId)
        {   
            ViewBag.Disciplines = _disciplineService.GetAllDisciplines();
            ViewBag.Courses = _courseService.GetCourseDropDown();
            ViewBag.InstanceId = competitionInstanceId;
            return View();
        }

        //POST: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventViewModel model, int competitionInstanceId, int competitionId)
        {
            ViewBag.Disciplines = _disciplineService.GetAllDisciplines();
            ViewBag.InstanceId = model.CompetitionInstanceId;
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid && model.Name != null)
            {
                try
                {
                    var task = await _eventService.AddAsync(model);
                }
                catch (Exception e)
                {
                    //Todo: return some error view
                }
                return RedirectToAction("CompetitionInstance","Admin", new { competitionId, competitionInstanceId });
            }
            return View(model);
        }

        //GET: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/Edit
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/Edit/{eventId}")]
        public async Task<IActionResult> Edit(int competitionId, int competitionInstanceId, int eventId)
        {
            ViewBag.Disciplines = _disciplineService.GetAllDisciplines();
            ViewBag.Courses = _courseService.GetCourseDropDown();
            var model = await _eventService.GetEventViewModelByIdAsync(eventId);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        //POST: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/Edit
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/Edit/{eventId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int competitionId, int competitionInstanceId, int eventId, EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _eventService.EditEventViewModelAsync(model);
                return RedirectToAction("CompetitionInstance","admin", new { competitionId, competitionInstanceId });
            }
            return View(model);

        }

        //GET: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/Details
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/Details/{eventId}")]
        public async Task<IActionResult> Details(int competitionId, int competitionInstanceId, int eventId)
        {
            var eventobj = await _eventService.GetEventByIdAsync(eventId);
            if (eventobj == null)
            {
                return NotFound();
            }
            return View(eventobj);
        }

        //GET: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/Delete
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/Delete/{eventId}")]
        public async Task<IActionResult> Delete(int competitionId, int competitionInstanceId, int eventId)
        {
            var entity = await _eventService.GetEventByIdAsync(eventId);
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        //POST: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/Delete
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/Delete/{eventId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int competitionId, int competitionInstanceId, Event model)
        {
            await _eventService.RemoveAsync(model.Id);
            return RedirectToAction("CompetitionInstance", "Admin", new { competitionId, competitionInstanceId });
        }

        #endregion

        #region Devices

        [HttpGet]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Devices")]
        public async Task<IActionResult> Devices(int eventId, int competitionId, int competitionInstanceId)
        {
            var devices = _deviceService.GetDevicesInEvent(eventId);
            var _event = await _eventService.GetEventByIdAsync(eventId);
            var competitionInstance = await _competitionService.GetCompetitionInstanceByIdAsync(competitionInstanceId);
            var competition = await _competitionService.GetCompetitionByIdAsync(competitionId);
            var data = new DevicesDto
            {
                DeviceInEventViewModels = devices,
                EventName = _event.Name,
                CompetitionInstanceName = competitionInstance.Name,
                CompetitionName = competition.Name
            };
            return View(data);
        }

        [HttpGet]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/UnassignDevice/{deviceId}")]
        public async Task<IActionResult> UnassignDevice(int deviceId, int eventId, int competitionId, int competitionInstanceId)
        {
            await _deviceService.RemoveDeviceInEventAsync(new DevicesInEvent { DeviceId = deviceId, EventId = eventId });
            return RedirectToAction("Devices", "Event", new { eventId });
        }

        [HttpGet]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/AssignDevice")]
        public async Task<IActionResult> AssignDevice(int eventId, int competitionId, int competitionInstanceId)
        {
            ViewBag.Devices = _deviceService.GetUnassignedDevicesForEvent(eventId);
            ViewBag.Event = await _eventService.GetEventByIdAsync(eventId);
            return View();
        }

        [HttpPost("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/AssignDevice")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignDevice(CreateDeviceInEventViewModel model, int competitionInstanceId, int competitionId)
        {
            await _deviceService.AddDeviceInEventAsync(model.DeviceId, model.EventId);
            return RedirectToAction("Devices", model.EventId);
        }

        #endregion

        #region Contestants

        [HttpGet]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Contestants")]
        public async Task<IActionResult> Contestants(string search, int eventId, int competitionId, int competitionInstanceId, int count = 10)
        {
            ViewBag.EventName =  _eventService.GetEventById(eventId).Name;
            ViewData["CurrentFilter"] = search;
            var contestants = _competitionService.GetContestantsInCompetitionInstanceAndEvent(competitionInstanceId, eventId);
            var competitionInstance = await _competitionService.GetCompetitionInstanceByIdAsync(competitionInstanceId);
            var competition = await _competitionService.GetCompetitionByIdAsync(competitionId);

            if (!String.IsNullOrEmpty(search))
            {
                var searchToUpper = search.ToUpper();
                contestants = contestants.Where(u => u.Name.ToUpper().Contains(searchToUpper));
            }

            var model = new ContestantsInCompetitionInstanceDTO
            {
                Competition = competition,
                CompetitionInstance = competitionInstance,
                Contestants = contestants.OrderBy(x => x.Name).Take(count)
            };
            return View(model);
        }

        [HttpGet]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/AddContestant")]
        public async Task<IActionResult> AddContestant(string search, int eventId, int competitionId, int competitionInstanceId, int count)
        {
            return View();
        }


        #endregion
    }
}