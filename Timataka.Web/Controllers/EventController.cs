using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Services;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.EventViewModels;
using Timataka.Core.Models.ViewModels.DeviceViewModels;

namespace Timataka.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IDisciplineService _disciplineService;
        private readonly ICourseService _courseService;
        private readonly IDeviceService _deviceService;

        public EventController(IEventService eventService,
                               IDisciplineService disciplineService,
                               ICourseService courseService,
                               IDeviceService deviceService)
        {
            _disciplineService = disciplineService;
            _eventService = eventService;
            _courseService = courseService;
            _deviceService = deviceService;
        }

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventViewModel model)
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
                return RedirectToAction("CompetitionInstance","Admin", new { CompetitionInstanceid = model.CompetitionInstanceId });
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int competitionId, EventViewModel model)
        {
            if (competitionId != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {

                var compInstanceId = await _eventService.EditEventViewModelAsync(model);
                return RedirectToAction("CompetitionInstance","admin", new { @competitionInstanceId = compInstanceId });
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
        public async Task<IActionResult> Delete(int competitionId, int competitionInstanceId, int? eventId)
        {
            if (eventId == null)
            {
                return NotFound();
            }
            var entity = await _eventService.GetEventByIdAsync((int)eventId);;
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        //POST: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/Delete
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int competitionId, int competitionInstanceId, int eventId)
        {
            await _eventService.RemoveAsync(eventId);
            return RedirectToAction("CompetitionInstance", "Admin", new { competitionInstanceId });
        }


        // **************** DEVICES FOR EVENTS ****************** //

        #region DEVICES FOR EVENTS

        [HttpGet]
        [Route("/Event/{eventId}/Devices")]
        public IActionResult Devices(int eventId)
        {
            var data = _deviceService.GetDevicesInEvent(eventId);
            ViewBag.Event = _eventService.GetEventByIdAsync(eventId).Result;
            return View(data);
        }

        [HttpGet]
        [Route("/Event/{eventId}/UnassignDevice/{deviceId}")]
        public async Task<IActionResult> UnassignDevice(int deviceId, int eventId)
        {
            await _deviceService.RemoveDeviceInEventAsync(new DevicesInEvent { DeviceId = deviceId, EventId = eventId });
            return RedirectToAction("Event", "Admin", new { eventId = eventId });
        }

        [HttpGet]
        [Route("/Event/{eventId}/AssignDevice")]
        public async Task<IActionResult> AssignDevice(int eventId)
        {
            ViewBag.Devices = _deviceService.GetDevices();
            ViewBag.Event = await _eventService.GetEventByIdAsync(eventId);
            return View();
        }

        [HttpPost("/Event/{eventId}/AssignDevice")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignDevice(CreateDeviceInEventViewModel model)
        {
            await _deviceService.AddDeviceInEventAsync(model.DeviceId, model.EventId);
            return RedirectToAction("Devices", model.EventId);
        }

        #endregion
    }
}