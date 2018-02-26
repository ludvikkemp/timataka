using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Timataka.Core.Models.Dto.AdminDTO;
using Timataka.Core.Services;

namespace Timataka.Web.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IMemoryCache _cache;

        public AdminController(IAdminService adminService, IMemoryCache cache)
        {
            _adminService = adminService;
            _cache = cache;
        }
        
        [Authorize(Roles = "Superadmin, Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            IEnumerable<UserDto> listOfUsers;

            if (!_cache.TryGetValue("listOfUsers", out listOfUsers))
            {
                listOfUsers = _adminService.GetUsers();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(8));

                _cache.Set("listOfUsers", listOfUsers, cacheEntryOptions);
            }
            
            return View(listOfUsers);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Roles()
        {
            return View();
        }

    }
}