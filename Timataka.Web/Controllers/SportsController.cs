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
    public class SportsController : Controller
    {
        private readonly ISportsRepository _repo;

        public SportsController(ISportsRepository repo)
        {
            _repo = repo;
        }

        // GET: Sports
        public IActionResult Index()
        {
            var sports = _repo.Get();
            return View(sports);
        }

        // GET: Sports/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var sport = await _repo.GetByIdAsync(id);
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
        public async Task<IActionResult> Create([Bind("Id,Name")] Sport sport)
        {
            if (ModelState.IsValid)
            {
                await _repo.InsertAsync(sport);                
                return RedirectToAction(nameof(Index));
            }
            return View(sport);
        }

        // GET: Sports/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var sport = await _repo.GetByIdAsync(id);
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
            //return View(sport);

            return View();
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
    }
}
