﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Timataka.Core.Models.Dto.AdminDTO;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.AccountViewModels;
using Timataka.Core.Models.ViewModels.AdminViewModels;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;
using Timataka.Core.Services;
using Timataka.Core.Data.Repositories;

namespace Timataka.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IAccountService _accountService;
        private readonly ISportService _sportService;
        private readonly IDisciplineService _disciplineService;
        private readonly IMemoryCache _cache;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICompetitionService _competitionService;
        private readonly IEventService _eventService;
        private readonly IHeatService _heatService;
        private readonly IClubService _clubService;
        private readonly ICourseService _courseService;


        public AdminController(IAdminService adminService,
            IAccountService accountService,
            IMemoryCache cache,
            ISportService sportService,
            IDisciplineService disciplineService,
            RoleManager<IdentityRole> roleManager,
            ICompetitionService competitionService,
            IEventService eventService,
            IHeatService heatService,
            IClubService clubService,
            ICourseService courseService)
        {
            _adminService = adminService;
            _cache = cache;
            _accountService = accountService;
            _sportService = sportService;
            _disciplineService = disciplineService;
            _roleManager = roleManager;
            _competitionService = competitionService;
            _eventService = eventService;
            _heatService = heatService;
            _clubService = clubService;
            _courseService = courseService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Users(string search)
        {
            ViewData["CurrentFilter"] = search;
            if (!_cache.TryGetValue("listOfUsers", out IEnumerable<UserDto> listOfUsers))
            {
                listOfUsers = _adminService.GetUsers();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(8));

                _cache.Set("listOfUsers", listOfUsers, cacheEntryOptions);
            }

            if (!String.IsNullOrEmpty(search))
            {
                var searchToUpper = search.ToUpper();
                listOfUsers = listOfUsers.Where(u => u.Username.ToUpper().Contains(searchToUpper)
                                                     || u.FirstName.ToUpper().Contains(searchToUpper)
                                                     || u.LastName.ToUpper().Contains(searchToUpper)
                                                     || u.Country.ToUpper().Contains(searchToUpper));
            }

            return View(listOfUsers);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult EditUser(string username)
        {
            if (username == null)
            {
                return new BadRequestObjectResult(null);
            }

            var userDto = _adminService.GetUserByUsername(username);
            ViewBag.Nations = _accountService.GetNationsListItems();

            if (userDto == null)
            {
                return new BadRequestResult();
            }

            return View(userDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> EditUser(UserDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.UpdateUser(model);
                if (result)
                {
                    _cache.Remove("listOfUsers");
                    return Redirect("~/admin/users");
                }
            }

            // Something went wrong!
            ViewBag.Nations = _accountService.GetNationsListItems();
            return View(model);
        }

        [Authorize(Roles = "Superadmin")]
        public IActionResult Roles()
        {
            var roles = _adminService.GetRoles();
            return View(roles);
        }

        [HttpGet]
        [Authorize(Roles = "Superadmin")]
        public IActionResult AddRole()
        {
            return View();
        }

        [Authorize(Roles = "Superadmin, Admin")]
        public IActionResult Sports()
        {   
            var sports = _sportService.GetAllSports();
            return View(sports);
        }

        [Authorize(Roles = "Superadmin, Admin")]
        [Route("Admin/Sport/{id}")]
        public IActionResult Sport(int id)
        {
            var sport = _sportService.GetSportById(id);
            sport.Wait();
            var dto = new SportDto
            {
                Disciplines = _disciplineService.GetDisciplinesBySportId(id),
                Sport = sport.Result
            };
            return View(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Superadmin, Admin")]
        public IActionResult AddRole(CreateRoleViewModel model)
        {
            var role = _roleManager.FindByNameAsync(model.Name);

            if (role.Result == null)
            {
                Task roleResult = _roleManager.CreateAsync(new IdentityRole(model.Name));
                roleResult.Wait();
                return Redirect("~/Admin/Roles");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Superadmin, Admin")]
        public IActionResult Competitions()
        {
            var competitions = _competitionService.GetAllCompetitions();
            return View(competitions);
        }

        [HttpGet]
        [Route("Admin/Competition/{id}")]
        [Authorize(Roles = "Superadmin, Admin")]
        public IActionResult Competition(int id)
        {
            var competition = _competitionService.GetCompetitionById(id);
            competition.Wait();
            var compDto = new CompetitionDto
            {
                Competiton = competition.Result,
                Instances = _competitionService.GetAllInstancesOfCompetition(id)
            };
            return View(compDto);
        }

        [HttpGet]
        [Route("Admin/Instance/{id}")]
        [Authorize(Roles = "Superadmin, Admin")]
        public IActionResult Instance(int id)
        {   
            var instanceTask = _competitionService.GetCompetitionInstanceById(id);
            instanceTask.Wait();

            var instance = new CompetitionsInstanceViewModel
            {
                Id = instanceTask.Result.Id,
                CompetitionId = instanceTask.Result.CompetitionId,
                DateFrom = instanceTask.Result.DateFrom,
                DateTo = instanceTask.Result.DateTo,
                Location = instanceTask.Result.Location,
                CountryId = instanceTask.Result.CountryId,
                Name = instanceTask.Result.Name,
                Status = instanceTask.Result.Status,
                Deleted = instanceTask.Result.Deleted
            };
            
            var events = _eventService.GetEventsByCompetitionInstanceId(id);
            
            var instanceDto = new CompetitionInstanceDTO
            {
                CompetitonInstance = instance,
                Events = events
            };

            return View(instanceDto);

        }

        [HttpGet]
        [Route("Admin/Event/{id}")]
        [Authorize(Roles = "Superadmin, Admin")]
        public IActionResult Event(int id)
        {
            var eventObj = _eventService.GetEventByIdAsync(id);
            eventObj.Wait();

            var eventDto = new EventDto()
            {
                Event = eventObj.Result,
                Heats = _heatService.GetHeatsForEvent(id)
            };

            return View(eventDto);
        }

        [HttpGet]
        [Route("Admin/Heat/{id}")]
        [Authorize(Roles = "Superadmin, Admin")]
        public IActionResult Heat(int id)
        {
            var heat = _heatService.GetHeatByIdAsync(id);
            heat.Wait();

            var heatDto = new HeatDto()
            {
                Heat = heat.Result,
                Contestants = _heatService.GetContestantsInHeat(heat.Result.Id),
                Users = _heatService.GetApplicationUsersInHeat(heat.Result.Id)
            };

            return View(heatDto);
        }

        [HttpGet]
        [Route("Admin/Personnel/{id}")]
        [Authorize(Roles = "Superadmin, Admin")]
        public IActionResult Personnel(int id)
        {
            var competition = _competitionService.GetCompetitionById(id);
            competition.Wait();

            var usersDto = _adminService.GetUsers();

            var assignedRoles = _competitionService.GetAllRolesForCompetition(id);

            var roles = new Role();

            var personnelDto = new PersonnelDto()
            {
                AssignedRoles = assignedRoles,
                Competition = competition.Result,
                Roles = roles,
                Users = usersDto
            };

            return View(personnelDto);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Clubs()
        {
            var clubs = _clubService.GetListOfCLubs();
            return View(clubs);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Courses()
        {
            var courses = _courseService.GetListOfCourses();
            return View(courses);
        }
    }
}