using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.Entities;
using Timataka.Core.Services;

namespace Timataka.Web.Controllers
{
    public class SportsController : Controller
    {
        private readonly ISportService _sportService;

        public SportsController(ISportService sportService)
        {
            _sportService = sportService;
        }


        // GET: Sports
        public IActionResult Index()
        {
            var sports = _sportService.GetAllSports();
            return View(sports);
        }

        // GET: Sports/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var sport = await _sportService.GetSportById(id);

            if (sport == null)
            {
                return NotFound();
            }
            return View(sport);
        }

        // GET: Sports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,Name")] Sport sport)
        {
            if (ModelState.IsValid && sport.Name != null)
            {
                try
                {
                    await _sportService.Add(sport);
                }
                catch (Exception e)
                {
                    //Todo: return some error view
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sport);
        }

        // GET: Sports/Edit/5
        public IActionResult Edit(int id)
        {
            var sport = _sportService.GetSportById(id);
            if (sport == null)
            {
                return NotFound();
            }
            return View(sport);
        }

        // POST: Sports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Sport sport)
        {
            if (id != sport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _sportService.Edit(sport);
                return RedirectToAction(nameof(Index));
            }
            return View(sport);

        }

        // GET: Sports/Delete/5
        public async Task <IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sport = await _sportService.GetSportById((int)id);
            if (sport == null)
            {
                return NotFound();
            }

            return View(sport);
        }

        // POST: Sports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sport = await _sportService.Remove((int)id);
            return RedirectToAction(nameof(Index));
        }
    }
}
