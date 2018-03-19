using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Services;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.EventViewModels;

namespace Timataka.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IDisciplineService _disciplineService;
        public EventController(IEventService eventService,
                               IDisciplineService disciplineService)
        {
            _disciplineService = disciplineService;
            _eventService = eventService;
        }

        public IActionResult Create(int instanceId)
        {   
            ViewBag.Disciplines = _disciplineService.GetAllDisciplines();
            ViewBag.InstanceId = instanceId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventViewModel model)
        {
            ViewBag.Disciplines = _disciplineService.GetAllDisciplines();
            ViewBag.InstanceId = model.CompetitionInstanceId;
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid && model.Name != null)
            {
                try
                {
                    var task = await _eventService.AddAsync(model);
                }
                catch (Exception e)
                {
                    //Todo: return some error view
                }
                return RedirectToAction("Instance","Admin", new { @id = model.CompetitionInstanceId });
            }
            return View(model);
        }

        // GET: Event/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var eventobj = await _eventService.GetEventByIdAsync(id);

            if (eventobj == null)
            {
                return NotFound();
            }
            return View(eventobj);
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Disciplines = _disciplineService.GetAllDisciplines();
            var model = await _eventService.GetEventViewModelByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var compInstanceId = await _eventService.EditEventViewModelAsync(model);
                //await _eventService.EditAsync(model);
                return RedirectToAction("Instance","admin", new { @id = compInstanceId });
            }
            return View(model);

        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventobj = await _eventService.GetEventByIdAsync((int)id);
            if (eventobj == null)
            {
                return NotFound();
            }

            return View(eventobj);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventobj = await _eventService.RemoveAsync((int)id);
            return RedirectToAction("Event","Admin", new { @id = 1 });
        }
    }
}