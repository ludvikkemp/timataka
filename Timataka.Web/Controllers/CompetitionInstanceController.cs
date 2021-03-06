﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Timataka.Core.Models.Dto.AdminDTO;
using Timataka.Core.Models.Dto.CompetitionInstanceDTO;
using Timataka.Core.Models.Dto.Contestant;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;
using Timataka.Core.Models.ViewModels.ContestantViewModels;
using Timataka.Core.Models.ViewModels.DeviceViewModels;
using Timataka.Core.Models.ViewModels.EventViewModels;
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
        private readonly IResultService _resultService;
        private readonly IChipService _chipService;
        private readonly IMemoryCache _cache;

        public CompetitionInstanceController(ICompetitionService competitionService,
            IMemoryCache cache,
            IAccountService accountService,
            IDeviceService deviceService,
            IEventService eventService,
            IMarkerService markerService,
            IAdminService adminService,
            IHeatService heatService,
            IResultService resultService,
            IChipService chipService
        )
        {
            _competitionService = competitionService;
            _cache = cache;
            _accountService = accountService;
            _deviceService = deviceService;
            _eventService = eventService;
            _markerService = markerService;
            _adminService = adminService;
            _heatService = heatService;
            _resultService = resultService;
            _chipService = chipService;
        }

        #region COMPETITION INSTANCE

        //GET: /Admin/Competition/{competitionId}/CompetitionInstance/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/Create")]
        public async Task<IActionResult> Create(int competitionId)
        {
            ViewBag.CompId = competitionId;
            ViewBag.CompetitionIds = _competitionService.GetAllCompetitions();
            ViewBag.Nations = _accountService.GetNationsListItems();
            var competition = await _competitionService.GetCompetitionByIdAsync(competitionId);
            var model = new CompetitionsInstanceViewModel
            {
                DateFrom = DateTime.Now.Date,
                DateTo = DateTime.Now.Date,
                Name = competition.Name + " " + DateTime.Now.Year.ToString()                
            };
            return View(model);
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
                    return Json(e.Message);
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
            var nonDeletedEvents = _eventService.GetNonDeletedEventsByCompetitionInstanceId((int)competitionInstanceId);
            var c = await _competitionService.GetCompetitionInstanceByIdAsync((int)competitionInstanceId);
            if (c == null)
            {
                return NotFound();
            }

            ViewBag.NonDeletedEvents = nonDeletedEvents;
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

        #region DEVICES

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
            List<EventViewModel> events = _eventService.GetEventsByCompetitionInstanceId(competitionInstanceId).ToList();
            events.Insert(0, new EventViewModel { Id = 0, Name = "Select" });
            ViewBag.Events = events;
            List<Device> devices = new List<Device> { new Device { Active = false, Id = 0, Name = "Select" } };
            ViewBag.Devices = devices;
            return View();
        }

        public JsonResult GetUnassignedDevices(int eventId)
        {
            List<Device> devices = _deviceService.GetUnassignedDevicesForEvent(eventId).ToList();
            if(devices.Count() != 0)
            {
                devices.Insert(0, new Device { Active = false, Id = 0, Name = "Select" });
            }
            ViewBag.Devices = devices;
            return Json(new SelectList(devices, "Id", "Name"));
        }

        [HttpPost]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/AssignDevice")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignDevice(CreateDeviceInEventViewModel model, int competitionInstanceId, int competitionId)
        {
            try
            {
                await _deviceService.AddDeviceInEventAsync(model.DeviceId, model.EventId);
            }
            catch(Exception e)
            {
                return Json(e.Message);
            }
            
            return RedirectToAction("Devices", new { competitionInstanceId, competitionId });
        }

        #endregion

        #region MARKERS

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Markers")]
        public async Task<IActionResult> Markers(int competitionId, int competitionInstanceId)
        {
            await _markerService.GetMarkersFromTimingDb(competitionInstanceId);
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
            if(await _markerService.AssignMarkerToHeatAsync(model) == false)
            {
                return Json("Guntime already set for heat");
            }
            return RedirectToAction("Markers", new { competitionInstanceId, competitionId });
        }


        #endregion

        #region CONTESTANTS



        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Contestants")]
        public async Task<IActionResult> Contestants(string search, int competitionInstanceId, int competitionId, int count = 10)
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
                Contestants = contestants.OrderBy(x => x.Name).Take(count)
            };
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/SelectContestant")]
        public async Task<IActionResult> SelectContestant(string search, int competitionInstanceId, int competitionId, int count = 10)
        {
            ViewData["CurrentFilter"] = search;
            var users = _adminService.GetUsers();
            var competition = await _competitionService.GetCompetitionByIdAsync(competitionId);
            var competitionInstance = await _competitionService.GetCompetitionInstanceByIdAsync(competitionInstanceId);

            if (!String.IsNullOrEmpty(search))
            {
                var searchToUpper = search.ToUpper();
                users = users.Where(u => u.Username.ToUpper().Contains(searchToUpper)
                                                     || u.FirstName.ToUpper().Contains(searchToUpper)
                                                     || u.LastName.ToUpper().Contains(searchToUpper));
            }

            var model = new SelectContestantViewModel
            {
                Users = users.OrderBy(x => x.FirstName).Take(count),
                CompetitionName = competition.Name,
                CompetitionInstanceName = competitionInstance.Name
            };
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/AddContestant/{userId}")]
        public async Task<IActionResult> AddContestant(int competitionInstanceId, int competitionId, string userId)
        {
            var models = _competitionService.GetAddContestantViewModelByCompetitionInstanceId(competitionInstanceId, userId);
            var user = await _adminService.GetUserByIdAsync(userId);
            ViewBag.UserName = user.FirstName + " " + user.MiddleName + " " + user.LastName;
            return View(models);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/AddContestant/{userId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContestant(List<AddContestantViewModel> model, int competitionInstanceId, int competitionId, string userId)
        {
            if (ModelState.IsValid)
            {
                foreach(var item in model)
                {
                    if (item.Add)
                    {    
                        var contestantInHeat = new ContestantInHeat
                        {
                            Bib = item.Bib,
                            HeatId = item.HeatId,
                            Modified = DateTime.Now,
                            Team = item.Team,
                            UserId = item.UserId
                        };
                        await _heatService.AddAsyncContestantInHeat(contestantInHeat);

                        if (item.ChipNumber > 0)
                        {
                            var chip = await _chipService.GetChipByNumberAsync(item.ChipNumber);
                            if (chip == null)
                            {
                                await _heatService.RemoveAsyncContestantInHeat(contestantInHeat);
                                return Json("There exists no chip with the chip number " + item.ChipNumber);
                            }
                            var chipinHeat = new ChipInHeat
                            {
                                ChipCode = chip.Code,
                                HeatId = item.HeatId,
                                UserId = item.UserId,
                                Valid = true
                            };
                            try
                            {
                                await _chipService.AssignChipToUserInHeatAsync(chipinHeat);
                            }
                            catch(Exception e)
                            {
                                await _heatService.RemoveAsyncContestantInHeat(contestantInHeat);
                                return Json(e.Message);
                            }
                        }
                        
                    }
                }

                return RedirectToAction("Contestants", "CompetitionInstance", new { @competitionId = competitionId, @competitionInstanceId = competitionInstanceId });
            }
            var user = await _adminService.GetUserByIdAsync(userId);
            ViewBag.UserName = user.FirstName + " " + user.MiddleName + " " + user.LastName;
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/EditContestant/{userId}")]
        public async Task<IActionResult> EditContestant(string userId, int competitionInstanceId, int competitionId)
        {
            var user = await _adminService.GetUserByIdAsync(userId);
            var events = _eventService.GetEventsByCompetitionInstanceIdAndUserId(competitionInstanceId, userId);
            var competitionInstance = await _competitionService.GetCompetitionInstanceByIdAsync(competitionInstanceId);
            var competiton = await _competitionService.GetCompetitionByIdAsync(competitionId);
            if (user != null)
            {
                var nationName = _adminService.GetCountryNameById((int) user.Nationality);
                var model = new EditContestantDto
                {
                    CompetitionName = competiton.Name,
                    CompetitionInstanceName = competitionInstance.Name,
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    NationId = user.Nationality,
                    Phone = user.Phone,
                    Nationality = nationName,
                    Events = events
                };
                return View(model);
            }
            return RedirectToAction("Contestants","CompetitionInstance");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/EditContestant/{userId}/Event/{eventId}")]
        public async Task<IActionResult> EditContestantInEvent(string userId, int competitionInstanceId, int competitionId, int eventId)
        {
            var user = await _adminService.GetUserByIdAsync(userId);
            var competitionInstance = await _competitionService.GetCompetitionInstanceByIdAsync(competitionInstanceId);
            var competiton = await _competitionService.GetCompetitionByIdAsync(competitionId);
            var _event = await _eventService.GetEventByIdAsync(eventId);
            var heats = _heatService.GetHeatsForEvent(eventId);
            var dto = _competitionService.GetEditContestantChipHeatResultDtoFor(userId, eventId, competitionInstanceId);
            var nationName = _adminService.GetNationalityById((int)user.Nationality);
            var model = new EditContestantInEventDto
            {
                CompetitionName = competiton.Name,
                CompetitionInstanceName = competitionInstance.Name,
                EventName = _event.Name,
                UserName = user.Username,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                NationId = user.Nationality,
                Nationality = nationName,
                Phone = user.Phone,
                HeatNumber = dto.HeatNumber,
                Bib = dto.Bib,
                ChipNumber = dto.ChipNumber,
                HeatId = dto.HeatId,
                OldHeatId = dto.HeatId,
                ContestantInHeatModified = dto.ContestantInHeatModified,
                Notes = dto.Notes,
                Status = dto.Status,
                Team = dto.Team,
                HeatsInEvent = heats,
                OldChipCode = dto.ChipCode
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/EditContestant/{userId}/Event/{eventId}")]
        public async Task<IActionResult> EditContestantInEvent(EditContestantInEventDto model, string userId, int competitionInstanceId, int competitionId, int eventId)
        {
            if (ModelState.IsValid)
            {
                // Get The Chip Contestants in About To Be Connected To
                var chip = await _chipService.GetChipByNumberAsync(model.ChipNumber);
                if (chip == null && model.ChipNumber != 0)
                {
                    return Json("Chip with this Number does not exist");  
                }

                // Get oldChipInHeat To Remove
                var oldChipInHeat = await _chipService.GetChipInHeatByCodeUserIdAndHeatId(model.OldChipCode, userId, model.OldHeatId);
                if (oldChipInHeat != null)
                {
                    var chipSuccess = await _chipService.RemoveChipInHeatAsync(oldChipInHeat);
                    if (!chipSuccess)
                    {
                        return Json("chipInHeatToRemove not successfully removed");
                    }
                }

                if(model.ChipNumber != 0)
                {
                    // Edit Fields in Contestants New Chip
                    chip.LastCompetitionInstanceId = competitionInstanceId;
                    chip.LastUserId = userId;
                    chip.LastSeen = DateTime.Now;
                    var chipEdit = await _chipService.EditChipAsync(chip);
                    if (chipEdit != true)
                    {
                        return Json("Edit Chip Failed");
                    }

                    // Create New ChipInHeat
                    var newChipInHeat = new ChipInHeat
                    {
                        UserId = userId,
                        ChipCode = chip.Code,
                        HeatId = model.HeatId,
                        Valid = true
                    };

                    // Assigning New ChipInHeat To User
                    try
                    {
                        await _chipService.AssignChipToUserInHeatAsync(newChipInHeat);
                    }
                    catch(Exception e)
                    {
                        return Json(e.Message);
                    }
                    
                   
                }

                // Get ContestantInHeat To Remove
                var contestantInHeat = await _heatService.GetContestantsInHeatByUserIdAndHeatIdAsync(userId, model.OldHeatId);
                if (contestantInHeat == null)
                {
                    return Json("contestantInHeat does not match this userId and heatId");
                }

                // Get Old Results to keep track of data before it is deleted
                var oldResult = await _resultService.GetResultAsync(userId, model.OldHeatId);
                if (oldResult == null)
                {
                    return Json("Result does not match this userId and heatId");
                }

                // Removes ContestantInHeat and Result
                await _heatService.RemoveAsyncContestantInHeat(contestantInHeat);

                // Create New ContestantIn Heat To Replace The Old One
                // New Result Will Be Created Automatically
                var newContestantInHeat = new ContestantInHeat
                {
                    HeatId = model.HeatId,
                    UserId = userId,
                    Bib = model.Bib,
                    Team = model.Team,
                    Modified = DateTime.Now
                };

                // Save newContestantInHeat In Database
                await _heatService.AddAsyncContestantInHeat(newContestantInHeat);

                // Get The New Result To Update Its Data
                var newResult = await _resultService.GetResultAsync(userId, newContestantInHeat.HeatId);
                if (newResult == null)
                {
                    return Json("newResult was not created which means newContestantInHeat was not created");
                }

                // Edit Field That Came From The Model
                newResult.Modified = DateTime.Now;
                newResult.HeatId = model.HeatId;
                newResult.Status = model.Status;
                newResult.Notes = model.Notes;
                newResult.UserId = userId;

                // Edit Fields That Came From The Old Result
                newResult.Name = oldResult.Name;
                newResult.Club = oldResult.Club;
                newResult.Country = oldResult.Country;
                newResult.Created = oldResult.Created;
                newResult.Gender = oldResult.Gender;
                newResult.FinalTime = oldResult.FinalTime;
                newResult.Nationality = oldResult.Nationality;

                // Save newResult In Database
                await _resultService.EditAsync(newResult);

                return RedirectToAction("Contestants", "CompetitionInstance", new {competitionId, competitionInstanceId});
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/EditUserInfo/{userId}/Event/{eventId}")]
        public async Task<IActionResult> EditUserInfo(string userId, int competitionInstanceId, int competitionId, int eventId)
        {
            if (userId == null)
            {
                return new BadRequestObjectResult(null);
            }

            var user = await _adminService.GetUserByIdAsync(userId);
            var userDto = _adminService.GetUserByUsername(user.Username);
            ViewBag.Nations = _accountService.GetNationsListItems();
            ViewBag.Nationalities = _accountService.GetNationalityListItems();

            if (userDto == null)
            {
                return new BadRequestResult();
            }

            return View(userDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/EditUserInfo/{userId}/Event/{eventId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUserInfo(string userId, int competitionInstanceId, int competitionId, int eventId, UserDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.UpdateUser(model);
                if (result)
                {
                    _cache.Remove("listOfUsers");
                    return RedirectToAction("EditContestantInEvent", "CompetitionInstance", new { competitionId, competitionInstanceId, userId, eventId });
                }
            }
            ViewBag.Nations = _accountService.GetNationsListItems();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/RemoveContestant/{userId}/Event/{eventId}")]
        public async Task<IActionResult> RemoveContestant(string userId, int competitionInstanceId, int competitionId, int eventId)
        {
            var dto = _competitionService.GetEditContestantChipHeatResultDtoFor(userId, eventId, competitionInstanceId);
            var model = _heatService.GetContestantInHeatById(dto.HeatId, userId);
            ViewBag.Contestant = await _adminService.GetUserByIdAsync(userId);
            ViewBag.Event = await _eventService.GetEventByIdAsync(eventId);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/RemoveContestant/{userId}/Event/{eventId}")]
        public async Task<IActionResult> RemoveContestant(ContestantInHeat model, string userId, int competitionInstanceId, int competitionId, int eventId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = _heatService.GetContestantInHeatById(model.HeatId, model.UserId);
                    await _heatService.RemoveAsyncContestantInHeat(entity);
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }

                //Remove all chips in heat entries for this user in the heat
                var chipInHeat = _chipService.GetChipsInHeatsForUserInHeat(model.UserId, model.HeatId);
                foreach (var item in chipInHeat)
                {
                    _chipService.RemoveChipInHeat(item);
                }
                return RedirectToAction("Contestants", "CompetitionInstance", new { eventId, competitionId, competitionInstanceId });
            }
            return View(model);
        }

        #endregion


    }
}