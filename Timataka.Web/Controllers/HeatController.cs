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

namespace Timataka.Web.Controllers
{
    public class HeatController : Controller
    {
        private readonly IHeatService _heatService;
        private readonly IAdminService _adminService;
        private readonly IMarkerService _markerService;

        public HeatController(IHeatService heatService,
                              IAdminService adminService,
                              IMarkerService markerService)
        {
            _heatService = heatService;
            _adminService = adminService;
            _markerService = markerService;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HeatViewModel model)
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
                return RedirectToAction("Event", "Admin", new { @eventId = model.EventId });
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int eventId, Heat entity)
        {
            if (eventId != entity.Id)
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
        public async Task<IActionResult> Delete(int competitionId, int competitionInstanceId, int eventId, int? heatId)
        {
            if (heatId == null)
            {
                return NotFound();
            }
            var entity = await _heatService.GetHeatByIdAsync((int)heatId);
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        //POST: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/Delete
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int heatId)
        {
            var heat = _heatService.GetHeatByIdAsync(heatId);
            var eventId = heat.Result.EventId;
            await _heatService.RemoveAsync(heatId);
            return RedirectToAction("Event", "Admin", new { eventId });

        }


        // **************** CONTESTANTS IN HEAT ****************** //


        //GET: /Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/Delete
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddContestant(int heatId, int eventId)
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContestant(ContestantsInHeatViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entitiy = new ContestantInHeat
                    {
                        Bib = model.Bib,
                        HeatId = model.HeatId,
                        Modified = model.Modified,
                        Team = model.Team,
                        UserId = model.UserId
                    };

                    await _heatService.AddAsyncContestantInHeat(entitiy);
                }
                catch(Exception e)
                {
                    return new BadRequestResult();
                }
                return RedirectToAction("Heat", "Admin", new { eventId = model.HeatId });
            }
            return View(model);
        }

        public IActionResult EditContestant(int heatId, string userId)
        {
            var entitiy = _heatService.GetContestantInHeatById(heatId,userId);
            
            var model = new ContestantsInHeatViewModel
            {
                Bib = entitiy.Bib,
                HeatId = entitiy.HeatId,
                Modified = entitiy.Modified,
                Team = entitiy.Team,
                UserId = entitiy.UserId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditContestant(ContestantsInHeatViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = new ContestantInHeat
                    {
                        UserId = model.UserId,
                        HeatId = model.HeatId,
                        Bib = model.Bib,
                        Team = model.Team,
                        Modified = DateTime.Now
                    };

                    await _heatService.EditAsyncContestantInHeat(entity);
                }
                catch (Exception e)
                {
                    return new BadRequestResult();
                }
                return RedirectToAction("Heat", "Admin", new { eventId = model.HeatId });
            }
            return View(model);
        }

        public IActionResult RemoveContestant(int heatId, string userId)
        {
            var modelList = _heatService.GetContestantsInHeat(heatId);
            var model = new ContestantsInHeatViewModel();

            foreach(var item in modelList)
            {
                if (item.HeatId == heatId && item.UserId == userId)
                {
                    model = item;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveContestant(ContestantsInHeatViewModel model)
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

        public IActionResult DetailsContestant(int heatId, string userId)
        {
            if(heatId == 0 || userId == null)
            {
                return new BadRequestResult();
            }

            var modelList = _heatService.GetContestantsInHeat(heatId);
            var model = new ContestantsInHeatViewModel();

            foreach (var item in modelList)
            {
                if(item.HeatId == heatId && item.UserId == userId)
                {
                    model = item;
                }
            }

            return View(model);
        }

        public IActionResult Markers(int id)
        {
            var assignedMarkers = _markerService.GetMarkersForHeat(id);

            var instanceID = 1;

            var allMarkers = _markerService.GetMarkersForCompetitionInstance(instanceID);
            ViewBag.HeatId = id;
            return View(assignedMarkers);
        }

        public IActionResult AssignMarker(int heatId, int markerId)
        {
            _markerService.AssignMarkerToHeat(markerId, heatId);
            return RedirectToAction("Markers", "Heat", new { eventId = heatId} );
        }

        public IActionResult EditMarker(int id)
        {
            var markers = _markerService.GetMarkersForHeat(id);
            ViewBag.HeatId = id;
            return View(markers);
        }

        public IActionResult RemoveMarker(int id)
        {
            var markers = _markerService.GetMarkersForHeat(id);
            ViewBag.HeatId = id;
            return View(markers);
        }

    }
}