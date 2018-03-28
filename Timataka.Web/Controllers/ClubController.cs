using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.ViewModels.ClubViewModels;
using Timataka.Core.Services;

namespace Timataka.Web.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubService _clubService;

        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
        }

        //GET: /Admin/Club/Create
        [HttpGet]
        [Authorize(Roles="Admin")]
        [Route("/Admin/Club/Create")]
        public IActionResult Create()
        {
            return View();
        }

        //POST: /Admin/Club/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Club/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateClubViewModel model)
        {
            if (ModelState.IsValid && model.Name != null)
            {
                var exists = await _clubService.ClubExistsAsync(model.Name);
                if (!exists)
                {
                    await _clubService.AddAsync(model);
                    return RedirectToAction("Clubs", "Admin");
                }
            }
            return View(model);
        }

        //GET: /Admin/Club/Edit/{clubId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Club/Edit/{clubId}")]
        public IActionResult Edit(int clubId)
        {
            var model = _clubService.GetClubViewModelById(clubId);
            if (model == null)
            {
                return NotFound();
            }
            
            return View(model);
        }

        //POST: /Admin/Club/Edit/{clubId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Club/Edit/{clubId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int clubId, EditClubViewModel model)
        {
            if (clubId != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _clubService.EditClubAsync(model);
                return RedirectToAction("Clubs", "Admin");
            }
            return View(model);
        }

        //GET: /Admin/Club/Details{clubId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Club/Details/{clubId}")]
        public IActionResult Details(int clubId)
        {
            var club = _clubService.GetClubViewModelById(clubId);
            if (club == null)
            {
                return NotFound();
            }
            return View(club);
        }

        //GET: /Admin/Club/Delete{clubId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Club/Delete/{clubId}")]
        public IActionResult Delete(int? clubId)
        {
            if (clubId == null)
            {
                return NotFound();
            }
            var c = _clubService.GetClubViewModelById((int)clubId);
            if (c == null)
            {
                return NotFound();
            }
            c.Id = (int)clubId;
            return View(c);
        }

        //POST: /Admin/Club/Delete/{clubId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Club/Delete/{clubId}")]
        public async Task<IActionResult> Delete(int id)
        {
            var c = await _clubService.RemoveAsync(id);
            return RedirectToAction("Clubs", "Admin");
        }

    }
}