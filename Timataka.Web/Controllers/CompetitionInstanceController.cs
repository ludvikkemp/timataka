using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.Dto.CompetitionInstanceDTO;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;
using Timataka.Core.Models.ViewModels.DeviceViewModels;
using Timataka.Core.Models.ViewModels.MarkerViewModels;
using Timataka.Core.Services;

namespace Timataka.Web.Controllers
{
    public class CompetitionInstanceController : Controller
    {
        private readonly ICompetitionService _competitionService;
        private readonly IAccountService _accountService;
        private readonly IDeviceService _deviceService;
        private readonly IEventService _eventService;
        private readonly IMarkerService _markerService;
        private readonly IAdminService _adminService;
        private readonly IHeatService _heatService;

        public CompetitionInstanceController(ICompetitionService competitionService, 
            IAccountService accountService,
            IDeviceService deviceService,
            IEventService eventService,
            IMarkerService markerService,
            IAdminService adminService,
            IHeatService heatService
        )
        {
            _competitionService = competitionService;
            _accountService = accountService;
            _deviceService = deviceService;
            _eventService = eventService;
            _markerService = markerService;
            _adminService = adminService;
            _heatService = heatService;
        }

        #region CompetitionInstance

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
                    await _competitionService.AddInstanceAsync(model);
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
                var compInstance = await _competitionService.GetCompetitionInstanceByIdAsync(model.Id);
                await _competitionService.EditInstanceAsync(compInstance, model);
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
            var c = await _competitionService.GetCompetitionInstanceByIdAsync(competitionInstanceId);

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

            var c = await _competitionService.GetCompetitionInstanceByIdAsync((int)competitionInstanceId);
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
            var instance = _competitionService.GetCompetitionInstanceByIdAsync(competitionInstanceId);
            var compId = instance.Result.CompetitionId;
            await _competitionService.RemoveInstanceAsync((int)competitionInstanceId);
            return RedirectToAction("Competition", "Admin", new { @competitionId = compId });

        }

        #endregion

        #region Devices

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Devices")]
        public async Task<IActionResult> Devices(int competitionId, int competitionInstanceId)
        {
            var devices = _deviceService.GetDevicesInCompetitionInstance(competitionInstanceId);
            var competitionInstance = await _competitionService.GetCompetitionInstanceByIdAsync(competitionInstanceId);
            var competiton = await _competitionService.GetCompetitionByIdAsync(competitionId);
            var data = new DevicesDto
            {
                DeviceInEventViewModels = devices,
                CompetitionName = competiton.Name,
                CompetitionInstanceName = competitionInstance.Name
            };
            return View(data);
        }

        [HttpGet]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/UnassignDevice/{deviceId}/{eventId}")]
        public async Task<IActionResult> UnassignDevice(int deviceId, int eventId, int competitionInstanceId, int competitionId)
        {
            await _deviceService.RemoveDeviceInEventAsync(new DevicesInEvent { DeviceId = deviceId, EventId = eventId });
            return RedirectToAction("Devices", "CompetitionInstance", new { competitionInstanceId, competitionId });
        }

        [HttpGet]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/AssignDevice")]
        public IActionResult AssignDevice(int competitionId, int competitionInstanceId)
        {
            ViewBag.Devices = _deviceService.GetDevices();
            ViewBag.Events = _eventService.GetEventsByCompetitionInstanceId(competitionInstanceId);
            return View();
        }

        [HttpPost]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/AssignDevice")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignDevice(CreateDeviceInEventViewModel model, int competitionInstanceId, int competitionId)
        {
            await _deviceService.AddDeviceInEventAsync(model.DeviceId, model.EventId);
            return RedirectToAction("Devices", new { competitionInstanceId, competitionId });
        }

        #endregion

        #region Markers

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Markers")]
        public async Task<IActionResult> Markers(int competitionId, int competitionInstanceId)
        {
            var markers = _markerService.GetMarkersForCompetitionInstance(competitionInstanceId);
            var competitionInstance = await _competitionService.GetCompetitionInstanceByIdAsync(competitionInstanceId);
            var competiton = await _competitionService.GetCompetitionByIdAsync(competitionId);
            var data = new MarkersDto
            {
                Markers = markers,
                CompetitionName = competiton.Name,
                CompetitionInstanceName = competitionInstance.Name
            };
            return View(data);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/AssignMarker/{markerId}")]
        public IActionResult AssignMarker(int competitionId, int competitionInstanceId, int markerId)
        {
            var data = _markerService.GetEventHeatListForMarker(markerId, competitionInstanceId);
            ViewBag.EventHeatList = data;
            ViewBag.markerId = markerId;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/AssignMarker/{markerId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignMarker(AssignMarkerToHeatViewModel model, int competitionInstanceId, int competitionId, int markerId)
        {
            await _markerService.AssignMarkerToHeatAsync(model);
            return RedirectToAction("Markers", new { competitionInstanceId, competitionId });
        }


        #endregion

        #region Contestants

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Contestants")]
        public async Task<IActionResult> Contestants(string search, int competitionInstanceId, int competitionId)
        {
            ViewData["CurrentFilter"] = search;
            var contestants = _competitionService.GetContestantsInCompetitionInstance(competitionInstanceId);
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
                Contestants = contestants
            };
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/EditContestant/{userId}")]
        public async Task<IActionResult> EditContestant(string userId, int competitionInstanceId, int competitionId)
        {
            var user = _adminService.GetUsers().SingleOrDefault(u => u.Id == userId);
            var events = _eventService.GetEventsByCompetitionInstanceIdAndUserId(competitionInstanceId, userId);
            

            if (user != null)
            {
                var nationality = _adminService.GetCountryNameById((int) user.NationalityId);
                var model = new EditContestantDto
                {
                    ContestantName = user.FirstName + " " + user.Middlename + " " + user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    NationId = user.NationalityId,
                    Phone = user.Phone,
                    Nationality = nationality,
                    //TODO: Ná í events:
                    Events = null
                };
                return View();
            }

            return RedirectToAction("Contestants","CompetitionInstance");
        }

        #endregion


    }
}