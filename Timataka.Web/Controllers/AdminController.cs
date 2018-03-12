using System;
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
using Timataka.Core.Services;

namespace Timataka.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IAccountService _accountService;
        private readonly ISportService _sportService;
        private readonly IDisciplineService _disciplineService;
        private readonly IMemoryCache _cache;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(IAdminService adminService, 
                                IAccountService accountService,
                                IMemoryCache cache,
                                ISportService sportService,
                                IDisciplineService disciplineService,
                                UserManager<ApplicationUser> userManager,
                                RoleManager<IdentityRole> roleManager)
        {
            _adminService = adminService;
            _cache = cache;
            _accountService = accountService;
            _sportService = sportService;
            _disciplineService = disciplineService;
            _userManager = userManager;
            _roleManager = roleManager;
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
                                                     || u.Email.ToUpper().Contains(searchToUpper)
                                                     || u.FirstName.ToUpper().Contains(searchToUpper)
                                                     || u.LastName.ToUpper().Contains(searchToUpper));
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

        [Authorize(Roles = "Superadmin, Admin")]
        public IActionResult Roles()
        {
            var roles = _adminService.GetRoles();
            return View(roles);
        }

        [HttpGet]
        [Authorize(Roles = "Superadmin, Admin")]
        public IActionResult AddRole()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Sports()
        {
            var sports = _sportService.GetAllSports();
            var disciplines = _disciplineService.GetAllDisciplines();

            List<SportsViewModel> models = new List<SportsViewModel>();

            //Collect all sports into viewModel
            foreach (var sport in sports)
            {
                var model = new SportsViewModel
                {
                    IsSport = true,
                    DisciplineId = 0,
                    DisciplineName = "",
                    SportId = sport.Id,
                    SportName = sport.Name
                };

                models.Add(model);
            }

            //Collect all disciplines into viewModel
            foreach (var discipline in disciplines)
            {
                var model = new SportsViewModel
                {
                    IsSport = false,
                    DisciplineId = discipline.Id,
                    DisciplineName = discipline.Name,
                    SportId = discipline.SportId,
                    SportName = ""
                };

                models.Add(model);
                
            }
            return View(models);

        }

        [HttpPost]
        [Authorize(Roles = "Superadmin, Admin")]
        public IActionResult AddRole(CreateRoleViewModel model)
        {
            /*
            var role = _roleManager.FindByNameAsync(model.Name);
            role.Wait();
            if (!role.IsCompletedSuccessfully)
            {
                Task roleResult = _roleManager.CreateAsync(new IdentityRole(model.Name));
                roleResult.Wait();
                return Redirect("~/Admin/Roles");
            }
            */
            return View(model);
        }

    }
}