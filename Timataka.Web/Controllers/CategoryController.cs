using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet]
        public async Task <IActionResult> Create(int id)
        {
            ViewBag.Events = _eventService.GetEventDropDownList();
            ViewBag.Nations = _accountService.GetNationsListItems();
            var _event = await _eventService.GetEventByIdAsync(id);
            ViewBag.EventName = _event.Name;
            ViewBag.EventId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (ModelState.IsValid && model.Name != null)
            {
                var exists = await _categoryService.CategoryExistsAsync(model.Name);
                if (!exists)
                {
                    await _categoryService.AddAsync(model);
                    return RedirectToAction("Categories", "Admin");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Events = _eventService.GetEventDropDownList();
            ViewBag.Nations = _accountService.GetNationsListItems();
            var model = await _categoryService.GetCategoryViewModelById(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryViewModel model)
        {
            ViewBag.Events = _eventService.GetEventDropDownList();
            ViewBag.Nations = _accountService.GetNationsListItems();
            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _categoryService.EditClubAsync(model);
                return RedirectToAction("Clubs", "Admin");
            }
            return View(model);
        }
    }
}