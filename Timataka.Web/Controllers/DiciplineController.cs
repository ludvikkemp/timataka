using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Timataka.Web.Controllers
{
    public class DiciplineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}