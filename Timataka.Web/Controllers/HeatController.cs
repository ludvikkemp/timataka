﻿using System;
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

namespace Timataka.Web.Controllers
{
    public class HeatController : Controller
    {
        private readonly IHeatService _heatService;
        private readonly IAdminService _adminService;
        private readonly IMarkerService _markerService;
        private readonly IChipService _chipService;

        public HeatController(IHeatService heatService,
                              IAdminService adminService,
                              IMarkerService markerService,
                              IChipService chipService)
        {
            _heatService = heatService;
            _adminService = adminService;
            _markerService = markerService;
            _chipService = chipService;
        }

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


        // **************** CONTESTANTS IN HEAT ****************** //


        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/AddContestant")]
        public IActionResult AddContestant(int heatId, int eventId, int competitionId, int competitionInstanceId)
        {
            List<SelectListItem> selectUsersListItems =
                new List<SelectListItem>();

            var listOfUsers = _adminService.GetUsers();

            foreach (var item in listOfUsers)
            {
                selectUsersListItems.Add(
                    new SelectListItem
                    {
                        Text = item.FirstName + ' ' + item.Middlename + ' ' + item.LastName + " (" + item.Ssn + ")",
                        Value = item.Id
                    });
            }

            ViewBag.heatId = heatId;
            ViewBag.users = selectUsersListItems;
            return View(); 
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/AddContestant")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContestant(AddContestantToEventViewModel model, int heatId, int eventId, int competitionId, int competitionInstanceId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entitiy = new ContestantInHeat
                    {
                        Bib = model.ContestantInEvent.Bib,
                        HeatId = model.ContestantInEvent.HeatId,
                        Modified = DateTime.Now,
                        Team = model.ContestantInEvent.Team,
                        UserId = model.UserId
                    };

                    await _heatService.AddAsyncContestantInHeat(entitiy);
                }
                catch(Exception e)
                {
                    return new BadRequestResult();
                }
                return RedirectToAction("Heat", "Admin", new { heatId, eventId, competitionId, competitionInstanceId });
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/EditContestant/{userId}")]
        public async Task<IActionResult> EditContestant(int heatId, int eventId, int competitionId, int competitionInstanceId, string userId)
        {
            var e = await _heatService.GetContestantInEventViewModelAsync(userId, heatId);
            
            var model = new EditContestantInCompetitionViewModel
            {
                

            };

            return View();
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
        public IActionResult RemoveContestant(int heatId, string userId)
        {
            var model = _heatService.GetContestantInHeatById(heatId, userId);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveContestant(ContestantInHeat model)
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
                return RedirectToAction("Heat", "Admin", new { eventId = model.HeatId });
            }
            return View(model);
        }

        #region Markers

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Markers")]
        public IActionResult Markers(int competitionId, int competitionInstanceId, int eventId, int heatId)
        {
            ViewBag.AssignedMarkers = _markerService.GetMarkersForHeat(heatId);
            ViewBag.HeatId = heatId;
            ViewBag.MarkerList = _markerService.GetUnAssignedMarkersForHeat(heatId, competitionInstanceId);
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Markers")]
        public async Task<IActionResult> AssignMarker(AssignMarkerToHeatViewModel model, int competitionId, int competitionInstanceId, int eventId, int heatId, int markerId)
        {
            await _markerService.AssignMarkerToHeatAsync(model);
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

        /*** CHIPS IN HEAT ***/

        //GET: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Chips
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Chips")]
        public IActionResult Chips(int heatId, int eventId, int competitionInstanceId, int competitionId)
        {
            var chipsInHeat = _chipService.GetChipsInHeat(heatId);
            
            return View(chipsInHeat);
        }

        //GET: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Chips/AssignChip
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Chips/AssignChip")]
        public IActionResult AssignChip(int heatId, int competitionId, int competitionInstanceId, int eventId)
        {
            var usersInHeat = _heatService.GetContestantsInHeat(heatId);
            var chips = _chipService.Get();

            List<SelectListItem> selectUsersListItems =
                new List<SelectListItem>();

            List<SelectListItem> selectChipsListItems =
                new List<SelectListItem>();

            foreach (var item in usersInHeat)
            {
                selectUsersListItems.Add(
                    new SelectListItem
                    {
                        Text = item.Name + " (" + item.Ssn + ")",
                        Value = item.UserId
                    });
            }

            foreach (var item in chips)
            {
                selectChipsListItems.Add(
                    new SelectListItem
                    {
                        Text = item.Code + " (" + item.Number + ")",
                        Value = item.Code
                    });
            }

            ViewBag.Users = selectUsersListItems;
            ViewBag.Chips = selectChipsListItems;
            
            return View();
        }

        //Post: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Chips/AssignChip
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Chips/AssignChip")]
        public IActionResult AssignChip(int heatId, int competitionId, int competitionInstanceId, int eventId, ChipInHeatViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new ChipInHeat
                {
                    ChipCode = model.ChipCode,
                    HeatId = model.HeatId,
                    Valid = model.Valid,
                    UserId = model.UserId
                };

                var status = _chipService.AssignChipToUserInHeat(entity);
                return RedirectToAction("Chips", "Heat", new { heatId = heatId, eventId = eventId, competitionInstanceId = competitionInstanceId, competitionId = competitionId });
            }

            return View(model);
        }

        //GET: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Chips/{chipCode}/EditChip/{userId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Chips/{chipCode}/EditChip/{userId}")]
        public IActionResult EditChip(int competitionId, int competitionInstanceId, int eventId, int heatId, string chipCode, string userId)
        {
            var model = _chipService.GetChipInHeatByCodeAndUserId(chipCode, userId, heatId);
               
            return View(model);
        }

        //Post: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Chips/{chipCode}/EditChip/{userId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Chips/{chipCode}/EditChip/{userId}")]
        [ValidateAntiForgeryToken]
        public IActionResult EditChip(ChipInHeatViewModel model, int competitionId, int competitionInstanceId, int eventId, int heatId, string chipCode, string userId)
        {
            if (ModelState.IsValid)
            {
                var entitiy = new ChipInHeat
                {
                    ChipCode = model.ChipCode,
                    HeatId = model.HeatId,
                    UserId = model.UserId,
                    Valid = model.Valid
                };

                var status = _chipService.EditChipInHeat(entitiy);
                return RedirectToAction("Chips", "Heat", new { heatId = heatId, eventId = eventId, competitionInstanceId = competitionInstanceId, competitionId = competitionId });
            }
            
            return View(model);
        }

        //GET: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Chips/{chipCode}/DetailsChip/{userId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Chips/{chipCode}/DetailsChip/{userId}")]
        public IActionResult DetailsChip(int competitionId, int competitionInstanceId, int eventId, int heatId, string chipCode, string userId)
        {
            var model = _chipService.GetChipInHeatByCodeAndUserId(chipCode, userId, heatId);
            return View(model);
        }

        //GET: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Chips/{chipCode}/RemoveChip/{userId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Chips/{chipCode}/RemoveChip/{userId}")]
        public IActionResult RemoveChip(int competitionId, int competitionInstanceId, int eventId, int heatId, string chipCode, string userId)
        {
            var model = _chipService.GetChipInHeatByCodeAndUserId(chipCode, userId, heatId);
            return View(model);
        }

        //Post: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Chips/{chipCode}/RemoveChip/{userId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}/Chips/{chipCode}/RemoveChip/{userId}")]
        public IActionResult RemoveChip(ChipInHeatViewModel model, int competitionId, int competitionInstanceId, int eventId, int heatId, string chipCode, string userId)
        {
            if(ModelState.IsValid)
            {
                var entitiy = new ChipInHeat
                {
                    ChipCode = model.ChipCode,
                    HeatId = model.HeatId,
                    UserId = model.UserId,
                    Valid = model.Valid
                };

                var status = _chipService.RemoveChipInHeat(entitiy);

                return RedirectToAction("Chips", "Heat", new { heatId = heatId, eventId = eventId, competitionInstanceId = competitionInstanceId, competitionId = competitionId });
            }

            return View(model);
        }
    }
}