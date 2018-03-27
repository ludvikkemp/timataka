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
        public IActionResult Create()
        {
            ViewBag.Events = _eventService.GetEventDropDownList();
            ViewBag.Nations = _accountService.GetNationsListItems();
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
                    return RedirectToAction("Categories", "Admin", new { @id = model.EventId });
                }
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = _categoryService.GetCategoryViewModelById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var status = await _categoryService.RemoveAsync(model.Id);
                return RedirectToAction("Categories", "Admin", new { @id = model.EventId });
            }
            return View(model);
        }
    }
}