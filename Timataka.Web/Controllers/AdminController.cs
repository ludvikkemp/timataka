using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Services;

namespace Timataka.Web.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        [Route("Users")]
        public IActionResult Users()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            
            return null;
        }
    }
}