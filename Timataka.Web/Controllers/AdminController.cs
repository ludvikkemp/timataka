using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Timataka.Web.Controllers
{
    
    public class AdminController : Controller
    {
        
        public AdminController()
        {

        }
        [Authorize(Roles = "Admin, Superadmin")]
        public IActionResult Index()
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