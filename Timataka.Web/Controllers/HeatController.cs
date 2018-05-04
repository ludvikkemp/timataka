using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.ViewModels.HeatViewModels;
using Timataka.Core.Services;
using Timataka.Core.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Timataka.Core.Models.Dto.HeatDTO;
using Timataka.Core.Models.ViewModels.ChipViewModels;
using Timataka.Core.Models.ViewModels.MarkerViewModels;
using Timataka.Core.Models.ViewModels.EventViewModels;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;
using Timataka.Core.Models.ViewModels;
using Timataka.Core.Models.ViewModels.ContestantViewModels;
using Timataka.Core.Models.ViewModels.HomeViewModels;

namespace Timataka.Web.Controllers
{
    public class HeatController : Controller
    {
        private readonly IHeatService _heatService;
        private readonly IAdminService _adminService;
        private readonly IMarkerService _markerService;
        private readonly IChipService _chipService;
        private readonly ICompetitionService _competitionService;
        private readonly IEventService _eventService;
        private readonly IResultService _resultService;

        public HeatController(IHeatService heatService,
            IAdminService adminService,
            IMarkerService markerService,
            IChipService chipService,
            ICompetitionService competitionService,
            IEventService eventService,
            IResultService resultService)
        {
            _heatService = heatService;
            _adminService = adminService;
            _markerService = markerService;
            _chipService = chipService;
            _competitionService = competitionService;
            _eventService = eventService;
            _resultService = resultService;
        }

        #region HEATS

        //GET: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/Create")]
        public IActionResult Create(int competitionId, int competitionInstanceId, int eventId)
        {
            return View();
        }

        //POST: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int competitionId, int competitionInstanceId, int eventId, HeatViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _heatService.AddAsync(model.EventId);
                }
                catch (Exception e)
                {
                    //Todo: return some error view
                }
                return RedirectToAction("Event", "Admin", new { competitionId, competitionInstanceId, eventId });
            }
            return View(model);
        }

        //GET: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/Edit
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/Edit/{heatId}")]
        public async Task<IActionResult> Edit(int competitionId, int competitionInstanceId, int eventId, int heatId)
        {
            var entity = await _heatService.GetHeatByIdAsync(heatId);
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        //POST: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/Edit
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/Edit/{heatId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int competitionId, int competitionInstanceId, int eventId, int heatId, Heat entity)
        {
            if (heatId != entity.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _heatService.EditAsync(entity);
                return RedirectToAction("Event", "Admin", new { @eventId = entity.EventId });
            }
            return View(entity);
        }

        //GET: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/Delete
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/Delete")]
        public async Task<IActionResult> Delete(int competitionId, int competitionInstanceId, int eventId, int heatId)
        {
            var entity = await _heatService.GetHeatByIdAsync(heatId);
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        //POST: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/Delete
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int competitionId, int competitionInstanceId, int eventId, Heat model)
        {
            await _heatService.RemoveAsync(model.Id);
            return RedirectToAction("Event", "Admin", new { competitionId, competitionInstanceId, eventId });

        }
        #endregion

        #region CONTESTANTS

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/SelectContestant")]
        public async Task<IActionResult> SelectContestant(string search, int heatId, int eventId, int competitionInstanceId, int competitionId, int count = 10)
        {
            ViewData["CurrentFilter"] = search;
            var users = _heatService.GetUsersNotInAnyHeatUnderEvent(eventId);

            var competition = await _competitionService.GetCompetitionByIdAsync(competitionId);
            var competitionInstance = await _competitionService.GetCompetitionInstanceByIdAsync(competitionInstanceId);
            var _event = await _eventService.GetEventByIdAsync(eventId);
            var heat = await _heatService.GetHeatByIdAsync(heatId);

            if (!String.IsNullOrEmpty(search))
            {
                var searchToUpper = search.ToUpper();
                users = users.Where(u => u.UserName.ToUpper().Contains(searchToUpper)
                                         || u.FirstName.ToUpper().Contains(searchToUpper)
                                         || u.LastName.ToUpper().Contains(searchToUpper));
            }

            var model = new SelectContestantViewModel2
            {
                Users = users.OrderBy(x => x.FirstName).Take(count),
                CompetitionName = competition.Name,
                CompetitionInstanceName = competitionInstance.Name,
                EventName = _event.Name,
                HeatNumber = heat.HeatNumber
            };
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/AddContestant/{userId}")]
        public async Task<IActionResult> AddContestant(int heatId, int eventId, int competitionId, int competitionInstanceId, string userId)
        {
            var user = await _adminService.GetUserByIdAsync(userId);
            ViewBag.UserName = user.FirstName + " " + user.LastName;
            var heat = await _heatService.GetHeatByIdAsync(heatId);
            ViewBag.HeatId = heat.HeatNumber;
            return View(); 
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/AddContestant/{userId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContestant(AddContestantToHeatViewModel model, int heatId, int eventId, int competitionId, int competitionInstanceId, string userId)
        {
            if (!ModelState.IsValid)
            {
                var user = await _adminService.GetUserByIdAsync(userId);
                ViewBag.UserName = user.FirstName + " " + user.LastName;
                var heat = await _heatService.GetHeatByIdAsync(heatId);
                ViewBag.HeatId = heat.HeatNumber;
                return View(model);
            }

            var entitiy = new ContestantInHeat
            {
                Bib = model.Bib,
                HeatId = heatId,
                Modified = DateTime.Now,
                Team = model.Team,
                UserId = model.UserId
            };
            await _heatService.AddAsyncContestantInHeat(entitiy);

            if (model.ChipNumber > 0)
            {
                var chip = await _chipService.GetChipByNumberAsync(model.ChipNumber);
                if (chip == null)
                {
                    return Json("Chipnumber Does Not Exist");
                }
                var chipinHeat = new ChipInHeat
                {
                    ChipCode = chip.Code,
                    HeatId = heatId,
                    UserId = model.UserId,
                    Valid = true
                };
                await _chipService.AssignChipToUserInHeatAsync(chipinHeat);
            }
            return RedirectToAction("Heat", "Admin", new { heatId, eventId, competitionId, competitionInstanceId });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/EditContestant/{userId}")]
        public async Task<IActionResult> EditContestant(int heatId, int eventId, int competitionId, int competitionInstanceId, string userId)
        {
            //Working on this!!!
            IEnumerable<ContestantInEventViewModel> events = Enumerable.Empty<ContestantInEventViewModel>();
            var e = await _heatService.GetContestantInEventViewModelAsync(userId, heatId);
            events.Append(e);
            var r = _resultService.GetResult(userId, heatId);
            var u = await _adminService.GetUserByIdAsync(userId);
            var model = new EditContestantInCompetitionViewModel
            {
                Name = r.Name,
                Nationality = u.Nationality,
                Phone = u.Phone,
                UserId = userId,
                Year = u.DateOfBirth.Year,
                Event = events
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditContestant(EditContestantInCompetitionViewModel model, int heatId, int eventId, int competitionId, int competitionInstanceId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = (from e in model.Event
                                 where e.HeatId == heatId
                                 select new ContestantInHeat
                                 {
                                    UserId = model.UserId,
                                    HeatId = e.HeatId,
                                    Bib = e.Bib,
                                    Team = e.Team,
                                    Modified = DateTime.Now
                                 }).SingleOrDefault();

                    await _heatService.EditAsyncContestantInHeat(entity);
                }
                catch (Exception e)
                {
                    return new BadRequestResult();
                }
                return RedirectToAction("Heat", "Admin", new { heatId, eventId, competitionId, competitionInstanceId });
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/RemoveContestant")]
        public async Task<IActionResult> RemoveContestant(int heatId, int eventId, int competitionId, int competitionInstanceId, string userId)
        {
            var model = _heatService.GetContestantInHeatById(heatId, userId);
            ViewBag.Contestant = await _adminService.GetUserByIdAsync(userId);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/RemoveContestant")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveContestant(ContestantInHeat model, int heatId, int eventId, int competitionId, int competitionInstanceId)
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
                    return new BadRequestResult();
                }
                //Remove all chips in heat entries for this user in the heat
                var chipInHeat = _chipService.GetChipsInHeatsForUserInHeat(model.UserId, heatId);
                foreach(var item in chipInHeat)
                {
                    _chipService.RemoveChipInHeat(item);
                }
                return RedirectToAction("Heat", "Admin", new { heatId, eventId, competitionId, competitionInstanceId });
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/DetailsContestant")]
        public async Task<IActionResult> DetailsContestant(int heatId, int eventId, int competitionId, int competitionInstanceId, string userId)
        {
            var entity = _heatService.GetContestantInHeatById(heatId, userId);

            var contestant = await _adminService.GetUserByIdAsync(userId);

            var heat = await _heatService.GetHeatByIdAsync(heatId);

            var model = new ContestantInHeatViewModel
            {
                Bib = entity.Bib,
                HeatId = entity.HeatId,
                Name = contestant.FirstName + " " + contestant.MiddleName + " " + contestant.LastName,
                DateOfBirth = contestant.DateOfBirth,
                Gender = contestant.Gender,
                HeatNumber = heat.HeatNumber,
                Phone = contestant.Phone,
                Ssn = contestant.Ssn,
                Team = entity.Team,
                UserId = entity.UserId
            };

            return View(model);
        }

        #endregion

        #region MARKERS

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Markers")]
        public async Task<IActionResult> Markers(int competitionId, int competitionInstanceId, int eventId, int heatId)
        {
            var assignedMarkers = _markerService.GetMarkersForHeat(heatId);
            var markerList = _markerService.GetUnAssignedMarkersForHeat(heatId, competitionInstanceId);
            var competition = await _competitionService.GetCompetitionByIdAsync(competitionId);
            var competitionInstance = await _competitionService.GetCompetitionInstanceByIdAsync(competitionInstanceId);
            var _event = await _eventService.GetEventByIdAsync(eventId);
            var heat = await _heatService.GetHeatByIdAsync(heatId);
            var data = new MarkerDto
            {
                AssignedMarkers = assignedMarkers,
                MarkerList = markerList,
                CompetitionName = competition.Name,
                CompetitionInstanceName = competitionInstance.Name,
                EventName = _event.Name,
                HeatNumber = heat.HeatNumber
            };
            return View(data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Markers")]
        public async Task<IActionResult> AssignMarker(AssignMarkerToHeatViewModel model, int competitionId, int competitionInstanceId, int eventId, int heatId, int markerId)
        {
            if(await _markerService.AssignMarkerToHeatAsync(model) == false)
            {
                return Json("Guntime already assigned");
            }
            return RedirectToAction("Markers", "Heat", new { competitionId, competitionInstanceId, eventId, heatId } );
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        [Route("Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Markers/{markerId}")]
        public async Task<IActionResult> UnassignMarker(int competitionId, int competitionInstanceId, int eventId, int heatId, int markerId)
        {
            await _markerService.UnassignMarkerAsync(new AssignMarkerToHeatViewModel { HeatId = heatId, MarkerId = markerId });
            return RedirectToAction("Markers", "Heat",  new { competitionId, competitionInstanceId, eventId, heatId });
        }

        #endregion

        #region RESULTS

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetTimes(int competitionId, int competitionInstanceId)
        {
            _resultService.GetTimes(competitionInstanceId);
            return RedirectToAction("CompetitionInstance", "Admin", new { competitionId = competitionId, competitionInstanceId = competitionInstanceId });
        }

        [HttpGet]
        [Authorize(Roles= "Admin")]
        public async Task<IActionResult> GetMarkers(int competitionId, int competitionInstanceId)
        {
            await _markerService.GetMarkersFromTimingDb(competitionInstanceId);
            return RedirectToAction("CompetitionInstance", "Admin", new { competitionId = competitionId, competitionInstanceId = competitionInstanceId });
        }

        //GET: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Results/{userId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Results/{userId}")]
        public IActionResult Results(int competitionId, int competitionInstanceId, int eventId, int heatId, string userId)
        {
            var entity = _resultService.GetResult(userId, heatId);
            var model = new ResultViewModel
            {
                UserId = entity.UserId,
                HeatId = entity.HeatId,
                Country = entity.Country,
                Nationality = entity.Nationality,
                Status = entity.Status,
                GunTime = entity.FinalTime,
                Gender = entity.Gender,
                Name = entity.Name,
                Club = entity.Club,
                Notes = entity.Notes,
                Created = entity.Created,
                Modified = entity.Modified
            };

            return View(model);
        }

        //Post: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Results/{userId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Results/{userId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Results(ResultViewModel model, int competitionId, int competitionInstanceId, int eventId, int heatId, string userId)
        {
            if (ModelState.IsValid)
            {
                var entitiy = new Result
                {
                    Modified = DateTime.Now,
                    Created = model.Created,
                    Notes = model.Notes,
                    Club = model.Club,
                    Name = model.Name,
                    Gender = model.Gender,
                    Country = model.Country,
                    FinalTime = model.GunTime,
                    HeatId =   model.HeatId,
                    Nationality = model.Nationality,
                    Status = model.Status,
                    UserId = model.UserId
                };

                var status = await _resultService.EditAsync(entitiy);
                return RedirectToAction("Heat", "Admin", new { heatId = heatId, eventId = eventId, competitionInstanceId = competitionInstanceId, competitionId = competitionId });
            }

            return View(model);
        }

        #endregion
    }
}