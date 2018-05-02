using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Timataka.Core.Models.Dto.AdminDTO;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;
using Timataka.Core.Services;
using Timataka.Core.Models.ViewModels.HeatViewModels;

namespace Timataka.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IAccountService _accountService;
        private readonly ISportService _sportService;
        private readonly IDisciplineService _disciplineService;
        private readonly IMemoryCache _cache;
        private readonly ICompetitionService _competitionService;
        private readonly IEventService _eventService;
        private readonly IHeatService _heatService;
        private readonly IClubService _clubService;
        private readonly ICourseService _courseService;
        private readonly IDeviceService _deviceService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ICategoryService _categoryService;
        private readonly IChipService _chipService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(IAdminService adminService,
            IAccountService accountService,
            IMemoryCache cache,
            ISportService sportService,
            IDisciplineService disciplineService,
            ICompetitionService competitionService,
            IEventService eventService,
            IHeatService heatService,
            IClubService clubService,
            ICourseService courseService,
            IDeviceService deviceService,
            IServiceProvider serviceProvider,
            ICategoryService categoryService,
            IChipService chipService,
            UserManager<ApplicationUser> userManager)

        {
            _adminService = adminService;
            _cache = cache;
            _accountService = accountService;
            _sportService = sportService;
            _disciplineService = disciplineService;
            _competitionService = competitionService;
            _eventService = eventService;
            _heatService = heatService;
            _clubService = clubService;
            _courseService = courseService;
            _deviceService = deviceService;
            _serviceProvider = serviceProvider;
            _categoryService = categoryService;
            _chipService = chipService;
            _userManager = userManager;
        }
            
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Admin/Users")]
        [Authorize(Roles = "Admin")]
        public IActionResult Users(string search, int count = 10)
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

            return View(listOfUsers.Take(count));
        }

        [HttpGet]
        [Route("Admin/User/Edit/{username}")]
        [Authorize(Roles = "Admin")]
        public ActionResult EditUser(string username)
        {
            if (username == null)
            {
                return new BadRequestObjectResult(null);
            }

            var userDto = _adminService.GetUserByUsername(username);
            ViewBag.Nations = _accountService.GetNationsListItems();
            ViewBag.Nationalities = _accountService.GetNationalityListItems();

            if (userDto == null)
            {
                return new BadRequestResult();
            }

            return View(userDto);
        }

        [HttpPost]
        [Route("Admin/User/Edit/{username}")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(string username, UserDto model)
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

        [HttpGet]
        [Route("Admin/Roles")]
        [Authorize(Roles = "Superadmin")]
        public async Task<IActionResult> Roles(string search)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewData["CurrentFilter"] = search;
            var adminUsers = _adminService.GetAdminUsers().Where(x => x.Username != user.UserName).ToList();
            var nonAdminUsers = _adminService.GetNonAdminUsers();

            if (!String.IsNullOrEmpty(search))
            {
                var searchToUpper = search.ToUpper();
                nonAdminUsers = nonAdminUsers.Where(u =>
                    u.FirstName.ToUpper().Contains(searchToUpper) || 
                    u.LastName.ToUpper().Contains(searchToUpper) ||
                    u.Username.ToUpper().Contains(searchToUpper));
            }

            var model = new UserRoleDto
            {
                Admins = adminUsers,
                Users = nonAdminUsers.Take(10)
            };
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Superadmin")]
        public async Task<IActionResult> AddRole(string id)
        {
            var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
            return RedirectToAction("Roles");
        }

        [HttpGet]
        [Authorize(Roles = "Superadmin")]
        public async Task<IActionResult> RemoveRole(string id)
        {
            var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                await userManager.RemoveFromRoleAsync(user, "Admin");
            }
            return RedirectToAction("Roles");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Sports(string search)
        {
            ViewData["CurrentFilter"] = search;
            var sports = _sportService.GetAllSports();

            if (!String.IsNullOrEmpty(search))
            {
                var searchToUpper = search.ToUpper();
                sports = sports.Where(u => u.Name.ToUpper().Contains(searchToUpper));
            }

            return View(sports);
        }

        [Authorize(Roles = "Admin")]
        [Route("Admin/Sport/{sportId}")]
        public IActionResult Sport(string search, int sportId)
        {
            ViewData["CurrentFilter"] = search;
            var sport = _sportService.GetSportByIdAsync(sportId);
            sport.Wait();
            var disciplines = _disciplineService.GetDisciplinesBySportId(sportId);

            if (!String.IsNullOrEmpty(search))
            {
                var searchToUpper = search.ToUpper();
                disciplines = disciplines.Where(u => u.Name.ToUpper().Contains(searchToUpper));
            }

            var dto = new SportDto
            {
                Disciplines = disciplines,
                Sport = sport.Result
            };
            return View(dto);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Competitions(string search)
        {
            ViewData["CurrentFilter"] = search;
            var competitions = _competitionService.GetAllCompetitions();

            if (!String.IsNullOrEmpty(search))
            {
                var searchToUpper = search.ToUpper();
                competitions = competitions.Where(u => u.Name.ToUpper().Contains(searchToUpper));
            }

            return View(competitions);
        }

        [HttpGet]
        [Route("Admin/Competition/{competitionId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Competition(string search, int competitionId)
        {
            ViewData["CurrentFilter"] = search;
            var competition = _competitionService.GetCompetitionByIdAsync(competitionId);
            competition.Wait();
            var competitionInstances = _competitionService.GetAllInstancesOfCompetition(competitionId);

            if (!String.IsNullOrEmpty(search))
            {
                var searchToUpper = search.ToUpper();
                competitionInstances = competitionInstances.Where(u => u.Name.ToUpper().Contains(searchToUpper));
            }

            var compDto = new CompetitionDto
            {
                Competiton = competition.Result,
                Instances = competitionInstances
            };
            return View(compDto);
        }

        [HttpGet]
        [Route("Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult CompetitionInstance(int competitionId, int competitionInstanceId)
        {   
            var instanceTask = _competitionService.GetCompetitionInstanceByIdAsync(competitionInstanceId);
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
            
            var events = _eventService.GetEventsByCompetitionInstanceId(competitionInstanceId);
            var competition = _competitionService.GetCompetitionByIdAsync(competitionId);
            competition.Wait();
            
            var instanceDto = new CompetitionInstanceDTO
            {
                Competition = competition.Result,
                CompetitionInstance = instance,
                Events = events
            };

            return View(instanceDto);

        }

        [HttpGet]
        [Route("Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Event(int competitionId, int competitionInstanceId, int eventId)
        {
            var eventObj = _eventService.GetEventByIdAsync(eventId);
            eventObj.Wait();

            var heats = _heatService.GetHeatsForEvent(eventId);
            
            var models = new List<HeatViewModel>();

            foreach(var heat in heats)
            {
                var model = new HeatViewModel
                {
                    Deleted = heat.Deleted,
                    EventId = heat.EventId,
                    HeatNumber = heat.HeatNumber,
                    Id = heat.Id,
                    NumberOfContestants = _heatService.GetContestantsInHeat(heat.Id).Count()
                };
                models.Add(model);
            }

            var competition = _competitionService.GetCompetitionByIdAsync(competitionId);
            competition.Wait();
            var competitionInstance = _competitionService.GetCompetitionInstanceByIdAsync(competitionInstanceId);
            competitionInstance.Wait();

            var eventDto = new EventDto()
            {
                Competition = competition.Result,
                CompetitionInstance = competitionInstance.Result,
                Event = eventObj.Result,
                Heats = models,
            };

            return View(eventDto);
        }

        [HttpGet]
        [Route("Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Heat/{heatId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Heat(string search, int competitionId, int competitionInstanceId, int eventId, int heatId)
        {
            ViewData["CurrentFilter"] = search;
            var heat = await _heatService.GetHeatByIdAsync(heatId);

            if (heat == null)
            {
                return NotFound();
            }

            var competition = await _competitionService.GetCompetitionByIdAsync(competitionId);
            var competitionInstance = await _competitionService.GetCompetitionInstanceByIdAsync(competitionInstanceId);
            var instanceEvent = await _eventService.GetEventByIdAsync(eventId);
            var contestants = _heatService.GetContestantsInHeat(heat.Id);

            if (!String.IsNullOrEmpty(search))
            {
                var searchToUpper = search.ToUpper();
                contestants = contestants.Where(u => u.Name.ToUpper().Contains(searchToUpper) 
                    || u.Ssn.ToUpper().Contains(searchToUpper));
            }

            var heatDto = new HeatDto()
            {
                Competition = competition,
                CompetitionInstance = competitionInstance,
                Event = instanceEvent,
                Heat = heat,
                Contestants = contestants
            };

            return View(heatDto);
        }

        [HttpGet]
        [Route("Admin/Competition/{competitionId}/Personnel")]
        [Authorize(Roles = "Admin")]
        public IActionResult Personnel(string search, int competitionId)
        {
            ViewData["CurrentFilter"] = search;
            var competition = _competitionService.GetCompetitionByIdAsync(competitionId);
            competition.Wait();
            var usersDto = _adminService.GetUsers().Take(10);
            if (!String.IsNullOrEmpty(search))
            {
                var searchToUpper = search.ToUpper();
                usersDto = usersDto.Where(u => u.FirstName.ToUpper().Contains(searchToUpper)
                                                     || u.LastName.ToUpper().Contains(searchToUpper)
                                                     || u.Username.ToUpper().Contains(searchToUpper));
            }
            var assignedRoles = _competitionService.GetAllRolesForCompetition(competitionId);
            var roles = new Role();
            var newUsersDto = new List<UserDto>();

            //TODO: Þetta tekur alltof langan tíma að loada 3000ms
            //TODO: Þarf mögulega að setja cache virkni hér þegar fleiri users bætast við
            foreach (var item in usersDto)
            {
                if (assignedRoles.FirstOrDefault(x => x.UserId == item.Id) == null)
                {
                    newUsersDto.Add(item);
                }
            }

            var personnelDto = new PersonnelDto()
            {
                AssignedRoles = assignedRoles,
                Competition = competition.Result,
                Roles = roles,
                Users = newUsersDto
            };

            return View(personnelDto);
        }

        [HttpGet]
        [Route("Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Catagories")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Categories(int competitionId, int competitionInstanceId, int eventId)
        {
            var categories = _categoryService.GetListOfCategoriesByEventId(eventId);
            var competition = await _competitionService.GetCompetitionByIdAsync(competitionId);
            var competitionInstance = await _competitionService.GetCompetitionInstanceByIdAsync(competitionInstanceId);
            var _event = await _eventService.GetEventByIdAsync(eventId);
            var data = new CategoryDto
            {
                CategoryViewModels = categories,
                CompetitionName = competition.Name,
                CompetitonInstanceName = competitionInstance.Name,
                EventName = _event.Name
            };
            return View(data);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Clubs(string search)
        {
            ViewData["CurrentFilter"] = search;
            var clubs = _clubService.GetListOfCLubs();

            if (!String.IsNullOrEmpty(search))
            {
                var searchToUpper = search.ToUpper();
                clubs = clubs.Where(u => u.Name.ToUpper().Contains(searchToUpper));
            }
            
            return View(clubs);
        }

        [HttpGet]
        [Route("Admin/Courses")]
        [Authorize(Roles = "Admin")]
        public IActionResult Courses(string search)
        {
            ViewData["CurrentFilter"] = search;
            var courses = _courseService.GetListOfCourseViewModels();

            if (!String.IsNullOrEmpty(search))
            {
                var searchToUpper = search.ToUpper();
                courses = courses.Where(u => u.Name.ToUpper().Contains(searchToUpper));
            }

            return View(courses);
        }

        [HttpGet]
        [Route("Admin/Devices")]
        [Authorize(Roles = "Admin")]
        public IActionResult Devices(string search)
        {
            ViewData["CurrentFilter"] = search;
            var devices = _deviceService.GetDevices();

            if (!String.IsNullOrEmpty(search))
            {
                var searchToUpper = search.ToUpper();
                devices = devices.Where(u => u.Name.ToUpper().Contains(searchToUpper));
            }

            return View(devices);

        }

        [HttpGet]
        [Route("Admin/Chips")]
        [Authorize(Roles = "Admin")]
        public IActionResult Chips(string search)
        {
            ViewData["CurrentFilter"] = search;
            var chips = _chipService.GetChips();

            if (!String.IsNullOrEmpty(search))
            {
                var searchToUpper = search.ToUpper();
                chips = chips.Where(u => u.Number.ToString().ToUpper().Contains(searchToUpper));
            }
            return View(chips);
        }
    }
}