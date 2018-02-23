using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.Entities;
using Timataka.Core.Services;

namespace Timataka.Web.Controllers
{
    public class SportsController : Controller
    {
        private readonly ISportService _sportsService;

        public SportsController(ISportService sportsService)
        {
            _sportsService = sportsService;
        }


        // GET: Sports
        public IActionResult Index()
        {
            var sports = _sportsService.GetAllSports();
            return View(sports);
        }

        // GET: Sports/Details/5
        public IActionResult Details(int id)
        {
            var sport = _sportsService.GetSportById(id);
            if (sport == null)
            {
                //return NotFound();
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
        public IActionResult Add([Bind("Id,Name")] Sport sport)
        {
            if (ModelState.IsValid && sport.Name != null)
            {
                _sportsService.Add(sport);                
                return RedirectToAction(nameof(Index));
            }
            return View(sport);
        }

        // GET: Sports/Edit/5
        public IActionResult Edit(int id)
        {
            var sport = _sportsService.GetSportById(id);
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
        public IActionResult Edit(int id, [Bind("Id,Name")] Sport sport)
        {
            if (id != sport.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(sport);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!SportExists(sport.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            return View(sport);

        }

        // GET: Sports/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var sport = await _context.Sports
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (sport == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(sport);
        //}

        //// POST: Sports/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var sport = await _context.Sports.SingleOrDefaultAsync(m => m.Id == id);
        //    _context.Sports.Remove(sport);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool SportExists(int id)
        //{
        //    return _context.Sports.Any(e => e.Id == id);
        //}
        public IActionResult Delete()
        {
            throw new NotImplementedException();
        }
    }
}
