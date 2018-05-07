using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Dto.AdminDTO;
using Timataka.Core.Models.Dto.CompetitionInstanceDTO;
using Timataka.Core.Services;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.EventViewModels;
using Timataka.Core.Models.ViewModels.DeviceViewModels;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;
using Timataka.Core.Models.ViewModels.ContestantViewModels;
using Timataka.Core.Models.ViewModels.CourseViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Timataka.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IDisciplineService _disciplineService;
        private readonly ICourseService _courseService;
        private readonly IDeviceService _deviceService;
        private readonly ICompetitionService _competitionService;
        private readonly IAdminService _adminService;
        private readonly IChipService _chipService;
        private readonly IHeatService _heatService;
        private readonly IResultService _resultService;
        private readonly IMemoryCache _cache;
        private readonly IAccountService _accountService;

        public EventController(IEventService eventService,
            IDisciplineService disciplineService,
            ICourseService courseService,
            IDeviceService deviceService,
            ICompetitionService competitionService,
            IAdminService adminService,
            IChipService chipService,
            IHeatService heatService,
            IResultService resultService,
            IAccountService accountService,
            IMemoryCache cache)
        {
            _disciplineService = disciplineService;
            _eventService = eventService;
            _courseService = courseService;
            _deviceService = deviceService;
            _competitionService = competitionService;
            _adminService = adminService;
            _chipService = chipService;
            _heatService = heatService;
            _resultService = resultService;
            _cache = cache;
            _accountService = accountService;
        }

        #region EVENT

        //GET: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/Create")]
        public async Task<IActionResult> Create(int competitionInstanceId, int competitionId)
        {   
            ViewBag.Disciplines = _disciplineService.GetAllDisciplines();
            ViewBag.Courses = _courseService.GetCourseDropDown();
            ViewBag.InstanceId = competitionInstanceId;
            var instance = await _competitionService.GetCompetitionInstanceByIdAsync(competitionInstanceId);
            var model = new EventViewModel
            {
                DateFrom = instance.DateFrom,
                DateTo = instance.DateTo,
                Laps = 1,
                Splits = 1
            };
            return View(model);
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
            if (ModelState.IsValid && model.Name != null)
            {
                try
                {
                    var task = await _eventService.AddAsync(model);
                }
                catch (Exception e)
                {
                    return Json(e.Message);
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
            var model = await _eventService.GetEventViewModelByIdAsync(eventId);
            if (model == null)
            {
                return NotFound();
            }
            ViewBag.Disciplines = _disciplineService.GetAllDisciplines();
            ViewBag.Courses = _courseService.GetCourseDropDown(model.DisciplineId);
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
            ViewBag.Contestants = _competitionService.GetContestantsInCompetitionInstanceAndEvent(competitionInstanceId, eventId);
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

        public JsonResult GetCoursesDropDown(int disciplineId)
        {
            List<CourseViewModelDropDownList> courses = _courseService.GetCourseDropDown(disciplineId).ToList();
            ViewBag.Courses = courses;
            return Json(new SelectList(courses, "Id", "Name"));
        }

        #endregion

        #region DEVICES

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

        #region CONTESTANTS

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
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/SelectContestant")]
        public async Task<IActionResult> SelectContestant(string search, int eventId, int competitionInstanceId, int competitionId, int count = 10)
        {
            ViewData["CurrentFilter"] = search;
            var users = _adminService.GetUsers();
            var competition = await _competitionService.GetCompetitionByIdAsync(competitionId);
            var competitionInstance = await _competitionService.GetCompetitionInstanceByIdAsync(competitionInstanceId);
            var _event = await _eventService.GetEventByIdAsync(eventId);

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
                CompetitionInstanceName = competitionInstance.Name,
                EventName = _event.Name
            };
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/AddContestant/{userId}")]
        public async Task<IActionResult> AddContestant(int competitionInstanceId, int competitionId, int eventId, string userId)
        {
            var models = _competitionService.GetAddContestantViewModelByCompetitionInstanceId(competitionInstanceId, userId);
            var user = await _adminService.GetUserByIdAsync(userId);
            ViewBag.UserName = user.FirstName + " " + user.MiddleName + " " + user.LastName;
            return View(models);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/AddContestant/{userId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContestant(List<AddContestantViewModel> model, int competitionInstanceId, int competitionId, int eventId, string userId)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in model)
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
                            catch (Exception e)
                            {
                                await _heatService.RemoveAsyncContestantInHeat(contestantInHeat);
                                return Json(e.Message);
                            }
                        }
                    }
                }

                return RedirectToAction("Contestants", "Event", new {competitionId, competitionInstanceId, @eventId = eventId });
            }
            var user = await _adminService.GetUserByIdAsync(userId);
            ViewBag.UserName = user.FirstName + " " + user.MiddleName + " " + user.LastName;
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/EditContestantInEvent/{userId}")]
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
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/EditContestantInEvent/{userId}")]
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
                var oldChipInHeat = _chipService.GetChipInHeatByCodeUserIdAndHeatId(model.OldChipCode, userId, model.OldHeatId);
                if (oldChipInHeat != null)
                {
                    var chipSuccess = _chipService.RemoveChipInHeat(oldChipInHeat);
                    if (!chipSuccess)
                    {
                        return Json("chipInHeatToRemove not successfully removed");
                    }
                }

                if (model.ChipNumber != 0)
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
                    var assignChipInHeat = _chipService.AssignChipToUserInHeat(newChipInHeat);
                    if (!assignChipInHeat)
                    {
                        return Json("Assingning New ChipInHeat To User Not Successful");
                    }
                }

                // Get ContestantInHeat To Remove
                var contestantInHeat = _heatService.GetContestantsInHeatByUserIdAndHeatId(userId, model.OldHeatId);
                if (contestantInHeat == null)
                {
                    return Json("contestantInHeat does not match this userId and heatId");
                }

                // Get Old Results to keep track of data before it is deleted
                var oldResult = _resultService.GetResult(userId, model.OldHeatId);
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
                var newResult = _resultService.GetResult(userId, newContestantInHeat.HeatId);
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

                return RedirectToAction("Contestants", "Event", new { competitionId, competitionInstanceId, eventId });
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/EditUserInfo/{userId}")]
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
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/EditUserInfo/{userId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUserInfo(string userId, int competitionInstanceId, int competitionId, int eventId, UserDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.UpdateUser(model);
                if (result)
                {
                    _cache.Remove("listOfUsers");
                    return RedirectToAction("EditContestantInEvent", "Event", new { competitionId, competitionInstanceId, userId, eventId });
                }
            }
            ViewBag.Nations = _accountService.GetNationsListItems();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/RemoveContestant/{userId}")]
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
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/RemoveContestant/{userId}")]
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
                return RedirectToAction("Contestants", "Event", new { eventId, competitionId, competitionInstanceId });
            }
            return View(model);
        }
        #endregion
    }
}