using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.DeviceViewModels;
using Timataka.Core.Services;

namespace Timataka.Web.Controllers
{
    public class DeviceController : Controller
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceServices)
        {
            _deviceService = deviceServices;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeviceViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _deviceService.AddAsync(model);
                    return RedirectToAction("Devices", "Admin");
                }
                catch
                {
                    //Todo
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _deviceService.GetDeviceByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Device model)
        {
            if(ModelState.IsValid)
            {
                await _deviceService.EditAsync(model);
                return RedirectToAction("Devices", "Admin");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var device = await _deviceService.GetDeviceByIdAsync(id);
            return View(device);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var device = await _deviceService.GetDeviceByIdAsync(id);
            return View(device);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _deviceService.RemoveAsync(id);
            return RedirectToAction("Devices", "Admin");
        }
               
    }
}