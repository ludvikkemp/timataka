using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timataka.Core.Data;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;

namespace Timataka.Web.Controllers
{
    public class DiciplineController : Controller
    {
        private readonly IDiciplineRepository _repo;

        public DiciplineController(IDiciplineRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var diciplines = _repo.Get();
            return View(diciplines);
        }


    }
}