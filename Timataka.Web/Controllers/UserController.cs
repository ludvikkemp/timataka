﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.Entities;
using Timataka.Core.Services;
using Timataka.Core.Models.Dto.UserDTO;
using Microsoft.AspNetCore.Authorization;

namespace Timataka.Web.Controllers
{
    public class UserController : Controller
    {

        private readonly ICompetitionService _competitionService;
        private readonly IEventService _eventService;
        private readonly IHeatService _heatService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(ICompetitionService competitionService, IEventService eventService, IHeatService heatService,
                              UserManager<ApplicationUser> userManager)
        {
            _competitionService = competitionService;
            _eventService = eventService;
            _heatService = heatService;
            _userManager = userManager;
        }

        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("User/Competitions")]
        [Authorize(Roles = "User")]
        public IActionResult Competitions(string search)
        {
            ViewData["CurrentFilter"] = search;
            var instances = _competitionService.GetAllCompetitionInstances() ;

            if(!String.IsNullOrEmpty(search))
            {
                instances = instances.Where(x => x.Name.ToUpper().Contains(search.ToUpper()));
            }

            instances = instances.OrderByDescending(x => x.DateFrom);

            return View(instances);
        }

        [HttpGet]
        [Route("User/Competitions/{competitionInstanceId}/Events")]
        [Authorize(Roles = "User")]
        public IActionResult Events(int competitionInstanceId)
        {
            var events = _eventService.GetEventsByCompetitionInstanceId(competitionInstanceId);
            ViewBag.userId = _userManager.GetUserAsync(User).Result.Id;

            events = events.OrderByDescending(x => x.DateFrom);
            return View(events);
        }

        [HttpGet]
        [Route("User/Competitions/Events/{eventId}/RegisterToEvent/{userId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RegisterToEvent(string userId, int eventId)
        {
            var eventObj = await _eventService.GetEventByIdAsync(eventId);
            var instance = await _competitionService.GetCompetitionInstanceByIdAsync(eventObj.CompetitionInstanceId);
            
            var model = new RegisterToEventDto
            {
                Event = eventObj,
                Instance = instance,
                UserId = userId
            };

            return View(model);
        }

        [HttpPost]
        [Route("User/Competitions/Events/{eventId}/RegisterToEvent/{userId}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RegisterToEvent(RegisterToEventDto model, string userId, int eventId)
        {
            if (ModelState.IsValid)
            {
                var heatsInEvent = _heatService.GetHeatsForEvent(model.Event.Id);

                var heatId = (from x in heatsInEvent
                              where x.HeatNumber == 0
                              select x.Id).SingleOrDefault();

                var contestantInHeat = new ContestantInHeat
                {
                    HeatId = heatId,
                    UserId = model.UserId,
                    Modified = DateTime.Now
                };

                await _heatService.AddAsyncContestantInHeat(contestantInHeat);

                return RedirectToAction("Events", "User", new { @competitionInstanceId = model.Instance.Id });
            }

            return View(model);
        }

        public IActionResult MyCompetitions()
        {

            return View();
        }

        public IActionResult Results()
        {
            return View();
        }

    }
}