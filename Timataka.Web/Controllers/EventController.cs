using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IActionResult Create(int instanceId)
        {   
            ViewBag.Disciplines = _disciplineService.GetAllDisciplines();
            ViewBag.Courses = _courseService.GetCourseDropDown();
            ViewBag.InstanceId = instanceId;
            return View();
        }

        [HttpPost]
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
                return RedirectToAction("Instance","Admin", new { @id = model.CompetitionInstanceId });
            }
            return View(model);
        }

        // GET: Event/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var eventobj = await _eventService.GetEventByIdAsync(id);

            if (eventobj == null)
            {
                return NotFound();
            }
            return View(eventobj);
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Disciplines = _disciplineService.GetAllDisciplines();
            ViewBag.Courses = _courseService.GetCourseDropDown();
            var model = await _eventService.GetEventViewModelByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var compInstanceId = await _eventService.EditEventViewModelAsync(model);
                //await _eventService.EditAsync(model);
                return RedirectToAction("Instance","admin", new { @id = compInstanceId });
            }
            return View(model);

        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _eventService.GetEventByIdAsync((int)id);;

            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instance = _eventService.GetEventByIdAsync(id);
            var instanceId = instance.Result.CompetitionInstanceId;
            await _eventService.RemoveAsync(id);
            return RedirectToAction("Instance", "Admin", new { @id = instanceId });

        }

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
            return RedirectToAction("Event", "Admin", new { @id = eventId });
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
    }
}