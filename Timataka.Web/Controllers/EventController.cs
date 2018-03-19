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

        public IActionResult Index(int eventId)
        {
            var eventObj = _eventService.GetEventById(eventId);

            return View(eventObj);
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
            if (ModelState.IsValid && model.Name != null)
            {
                try
                {
                    await _eventService.Add(model);
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
            var eventobj = await _eventService.GetEventById(id);

            if (eventobj == null)
            {
                return NotFound();
            }
            return View(eventobj);
        }

        // GET: Event/Edit/5
        public IActionResult Edit(int id)
        {
            var eventobj = _eventService.GetEventById(id);
            if (eventobj == null)
            {
                return NotFound();
            }
            return View(eventobj);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Event eventobj)
        {
            if (id != eventobj.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _eventService.Edit(eventobj);
                return RedirectToAction(nameof(Index));
            }
            return View(eventobj);

        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventobj = await _eventService.GetEventById((int)id);
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
            var eventobj = await _eventService.Remove((int)id);
            return RedirectToAction(nameof(Index));
        }
    }
}