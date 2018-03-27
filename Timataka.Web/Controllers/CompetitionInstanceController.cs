using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;
using Timataka.Core.Models.ViewModels.DeviceViewModels;
using Timataka.Core.Services;

namespace Timataka.Web.Controllers
{
    public class CompetitionInstanceController : Controller
    {
        private readonly ICompetitionService _competitionService;
        private readonly IAccountService _accountService;
        private readonly IDeviceService _deviceService;
        private readonly IEventService _eventService;

        public CompetitionInstanceController(ICompetitionService competitionService, 
                                             IAccountService accountService,
                                             IDeviceService deviceService,
                                             IEventService eventService)
        {
            _competitionService = competitionService;
            _accountService = accountService;
            _deviceService = deviceService;
            _eventService = eventService;
        }

        // Get: CompetitionInstances/Details/3
        public async Task<IActionResult> Details(int id)
        {
            var c = await _competitionService.GetCompetitionInstanceById(id);

            if (c == null)
            {
                return NotFound();
            }
            return View(c);
        }

        // Get: CompetitionInstances/Create
        public IActionResult Create(int compId)
        {
            ViewBag.CompId = compId;
            ViewBag.CompetitionIds = _competitionService.GetAllCompetitions();
            ViewBag.Nations = _accountService.GetNationsListItems();
            return View();
        }

        // Post: CompetitionInstances/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompetitionsInstanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _competitionService.AddInstance(model);
                }
                catch (Exception e)
                {
                    return new BadRequestResult();
                }
                return RedirectToAction("Competition","Admin", new { @id = model.CompetitionId });
            }
            return View(model);
        }

        // Get: CompetitionInstances/Edit/3
        public IActionResult Edit(int id)
        {
            var model = _competitionService.GetCompetitionInstanceViewModelById(id);
            ViewBag.ListOfNations = _accountService.GetNationsListItems();
            return View(model);
        }

        // Post: CompetitonInstances/Edit/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompetitionsInstanceViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var compInstance = await _competitionService.GetCompetitionInstanceById(model.Id);
                await _competitionService.EditInstance(compInstance, model);
                return RedirectToAction("Competition", "Admin", new { @id = compInstance.CompetitionId });
            }
            return View(model);
        }

        // GET: CompetitonInstances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var c = await _competitionService.GetCompetitionInstanceById((int)id);
            if (c == null)
            {
                return NotFound();
            }

            return View(c);
        }

        // POST: CompetitonInstances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instance = _competitionService.GetCompetitionInstanceById(id);
            var compId = instance.Result.CompetitionId;
            await _competitionService.RemoveInstance((int)id);
            return RedirectToAction("Competition", "Admin", new { @id = compId });

        }

        [HttpGet]
        [Route("/CompetitionInstance/{instanceId}/Devices")]
        public IActionResult Devices(int instanceId)
        {
            var data = _deviceService.GetDevicesInCompetitionInstance(instanceId);
            ViewBag.CompetitionInstance = _competitionService.GetCompetitionInstanceById(instanceId).Result;
            return View(data);
        }

        [HttpGet]
        [Route("/CompetitionInstance/{instanceId}/Event/{eventId}/UnassignDevice/{deviceId}")]
        public async Task<IActionResult> UnassignDevice(int deviceId, int eventId, int instanceId)
        {
            await _deviceService.RemoveDeviceInEventAsync(new DevicesInEvent { DeviceId = deviceId, EventId = eventId });
            return RedirectToAction("Devices", instanceId);
        }

        [HttpGet]
        [Route("/CompetitionInstance/{instanceId}/AssignDevice")]
        public IActionResult AssignDevice(int instanceId)
        {
            ViewBag.Devices = _deviceService.GetDevices();
            ViewBag.Events = _eventService.GetEventsByCompetitionInstanceId(instanceId);
            return View();
        }

        [HttpPost("/CompetitionInstance/{instanceId}/AssignDevice")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignDevice(CreateDeviceInEventViewModel model, int instanceId)
        {
            await _deviceService.AddDeviceInEventAsync(model.DeviceId, model.EventId);
            return RedirectToAction("Devices", instanceId);
        }


    }
}