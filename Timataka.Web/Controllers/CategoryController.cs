using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.ViewModels.CategoryViewModels;
using Timataka.Core.Services;

namespace Timataka.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IEventService _eventService;
        private readonly IAccountService _accountService;

        public CategoryController(
            ICategoryService categoryService,
            IEventService eventService,
            IAccountService accountService)
        {
            _categoryService = categoryService;
            _eventService = eventService;
            _accountService = accountService;
        }

        // Get: Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Catagories/Create
        [HttpGet]
        [Route("Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Catagories/Create")]
        [Authorize(Roles = "Admin")]
        public async Task <IActionResult> Create(int competitionId, int competitionInstanceId, int eventId)
        {
            ViewBag.Events = _eventService.GetEventDropDownList();
            ViewBag.Nations = _accountService.GetNationsListItems();
            var _event = await _eventService.GetEventByIdAsync(eventId);
            ViewBag.EventName = _event.Name;
            ViewBag.EventId = eventId;
            return View();
        }

        // Post: Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Catagories/Create
        [HttpPost]
        [Route("Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Catagories/Create")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel model, int competitionId, int competitionInstanceId, int eventId)
        {
            if (ModelState.IsValid && model.Name != null)
            {
                var exists = await _categoryService.CategoryExistsAsync(model.Name);
                if (!exists)
                {
                    await _categoryService.AddAsync(model);
                    return RedirectToAction("Categories", "Admin", new { competitionId = competitionId, competitionInstanceId = competitionInstanceId, eventId = eventId});
                }
            }
            return View(model);
        }

        // Get: Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Categories/{categoryId}/Edit
        [HttpGet]
        [Route("Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Categories/{categoryId}/Edit")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int competitionId, int competitionInstanceId, int eventId, int categoryId)
        {
            ViewBag.Events = _eventService.GetEventDropDownList();
            ViewBag.Nations = _accountService.GetNationsListItems();
            var model = await _categoryService.GetCategoryViewModelById(categoryId);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // Post: Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Categories/{categoryId}/Edit
        [HttpPost]
        [Route("Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Categories/{categoryId}/Edit")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int competitionId, int competitionInstanceId, int eventId, int categoryId, CategoryViewModel model)
        {
            if (categoryId != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _categoryService.EditCategoryAsync(model);
                return RedirectToAction("Categories", "Admin", new { competitionId = competitionId, competitionInstanceId = competitionInstanceId, eventId = eventId });
            }
            return View(model);
        }

        // Get: Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Categories/{categoryId}/Delete
        [HttpGet]
        [Route("Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Categories/{categoryId}/Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int competitionId, int competitionInstanceId, int eventId, int categoryId)
        {
            var model = await _categoryService.GetCategoryViewModelById(categoryId);
            return View(model);
        }

        // Post: Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Categories/{categoryId}/Delete
        [HttpPost]
        [Route("Admin/Competition/{competitionId}/CompetitionInstance/{competitionInstanceId}/Event/{eventId}/Categories/{categoryId}/Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CategoryViewModel model, int competitionId, int competitionInstanceId, int eventId, int categoryId)
        {
            if (ModelState.IsValid)
            {
                var status = await _categoryService.RemoveAsync(model.Id);
                return RedirectToAction("Categories", "Admin", new { competitionId = competitionId, competitionInstanceId = competitionInstanceId, eventId = eventId });
            }
            return View(model);
        }
        
    }
}