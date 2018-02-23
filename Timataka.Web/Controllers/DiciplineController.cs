﻿using System;
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
        private readonly IDiciplinesRepository _repo;

        public DiciplineController(IDiciplinesRepository repo)
        {
            _repo = repo;
        }

        //GET: Dicipline
        public IActionResult Index()
        {
            var diciplines = _repo.Get();
            return View(diciplines);
        }

        //GET: Dicipline/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Dicipline/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Dicipline dicipline)
        {
            if (ModelState.IsValid)
            {
                await _repo.InsertAsync(dicipline);
                return RedirectToAction(nameof(Index));
            }
            return View(dicipline);
        }

        //GET: Dicipline/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var dicipline = await _repo.GetByIdAsync(id);
            if (dicipline == null)
            {
                return NotFound();
            }

            return View(dicipline);
        }

        //GET: Dicipline/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var dicipline = await _repo.GetByIdAsync(id);
            if (dicipline == null)
            {
                return NotFound();
            }
            return View(dicipline);
        }

        //TODO
        //POST: Dicipline/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] Dicipline dicipline)
        {
            if (id != dicipline.Id)
            {
                return NotFound();
            }

            //TODO

            return View();
        }

        //TODO
        public IActionResult Delete()
        {
            throw new NotImplementedException();
        }
    }
}