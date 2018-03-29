using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        //GET: /Admin/Competition/{competitionId}/CompetitionInstance/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/Create")]
        public IActionResult Create(int competitionId)
        {
            ViewBag.CompId = competitionId;
            ViewBag.CompetitionIds = _competitionService.GetAllCompetitions();
            ViewBag.Nations = _accountService.GetNationsListItems();
            return View();
        }

        //GET: /Admin/Competition/{competitionId}/CompetitionInstance/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/Create")]
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
                return RedirectToAction("Competition","Admin", new { @competitionId = model.CompetitionId });
            }
            return View(model);
        }

        //GET: /Admin/Competition/{competitionId}/CompetitionInstance/Edit/{competitionInstanceId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/Edit/{competitionInstanceId}")]
        public IActionResult Edit(int competitionId, int competitionInstanceId)
        {
            var model = _competitionService.GetCompetitionInstanceViewModelById(competitionInstanceId);
            ViewBag.ListOfNations = _accountService.GetNationsListItems();
            return View(model);
        }

        //POST: /Admin/Competition/{competitionId}/CompetitionInstance/Edit/{competitionInstanceId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/Edit/{competitionInstanceId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int competitionId, int competitionInstanceId, CompetitionsInstanceViewModel model)
        {
            if (competitionInstanceId != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var compInstance = await _competitionService.GetCompetitionInstanceById(model.Id);
                await _competitionService.EditInstance(compInstance, model);
                return RedirectToAction("Competition", "Admin", new { @competitionId = compInstance.CompetitionId });
            }
            return View(model);
        }

        //GET: /Admin/Competition/{competitionId}/CompetitionInstance/Details/{competitionInstanceId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/Details/{competitionInstanceId}")]
        public async Task<IActionResult> Details(int competitionId, int competitionInstanceId)
        {
            var c = await _competitionService.GetCompetitionInstanceById(competitionInstanceId);

            if (c == null)
            {
                return NotFound();
            }
            return View(c);
        }

        //GET: /Admin/Competition/{competitionId}/CompetitionInstance/Delete/{competitionInstanceId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/Delete/{competitionInstanceId}")]
        public async Task<IActionResult> Delete(int competitionId, int? competitionInstanceId)
        {
            if (competitionInstanceId == null)
            {
                return NotFound();
            }

            var c = await _competitionService.GetCompetitionInstanceById((int)competitionInstanceId);
            if (c == null)
            {
                return NotFound();
            }

            return View(c);
        }

        //POST: /Admin/Competition/{competitionId}/CompetitionInstance/Delete/{competitionInstanceId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/Delete/{competitionInstanceId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int competitionId, int competitionInstanceId)
        {
            var instance = _competitionService.GetCompetitionInstanceById(competitionInstanceId);
            var compId = instance.Result.CompetitionId;
            await _competitionService.RemoveInstance((int)competitionInstanceId);
            return RedirectToAction("Competition", "Admin", new { @competitionId = compId });

        }


        // **************** DEVICES COMPETITION INSTANCES ****************** //


        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Devices")]
        public IActionResult Devices(int competitionId, int competitionInstanceId)
        {
            //TODO: HÉR ÞARF AÐ ATHUGA MEÐ RESPONSE TIME, TEKUR 1500ms REQUEST FYRIR 3-4 DEVICE AÐ LOADAST

            var data = _deviceService.GetDevicesInCompetitionInstance(competitionInstanceId);
            ViewBag.CompetitionInstance = _competitionService.GetCompetitionInstanceById(competitionInstanceId).Result;
            return View(data);
        }

        [HttpGet]
        [Route("/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/UnassignDevice/{deviceId}")]
        public async Task<IActionResult> UnassignDevice(int deviceId, int eventId, int competitionInstanceId)
        {
            await _deviceService.RemoveDeviceInEventAsync(new DevicesInEvent { DeviceId = deviceId, EventId = eventId });
            return RedirectToAction("Devices", "CompetitionInstance", new { competitionInstanceId });
        }

        [HttpGet]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/AssignDevice")]
        public IActionResult AssignDevice(int competitionId, int competitionInstanceId)
        {
            ViewBag.Devices = _deviceService.GetDevices();
            ViewBag.Events = _eventService.GetEventsByCompetitionInstanceId(competitionInstanceId);
            return View();
        }

        [HttpPost("/CompetitionInstance/{competitionInstanceId}/AssignDevice")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignDevice(CreateDeviceInEventViewModel model, int competitionInstanceId)
        {
            await _deviceService.AddDeviceInEventAsync(model.DeviceId, model.EventId);
            return RedirectToAction("Devices", competitionInstanceId);
        }


    }
}