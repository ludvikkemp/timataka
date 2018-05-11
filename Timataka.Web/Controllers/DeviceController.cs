using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        // Get: Admin/Devices/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("Admin/Devices/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // Post: Admin/Devices/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Admin/Devices/Create")]
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
                catch(Exception e)
                {
                    return Json(e.Message);
                }
            }
            return View(model);
        }

        // Get: Admin/Device/Edit/{deviceId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("Admin/Device/Edit/{deviceId}")]
        public async Task<IActionResult> Edit(int deviceId)
        {
            var model = await _deviceService.GetDeviceByIdAsync(deviceId);
            return View(model);
        }

        // Post: Admin/Device/Edit/{deviceId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Admin/Device/Edit/{deviceId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int deviceId, Device model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _deviceService.EditAsync(model);
                }
                catch(Exception e)
                {
                    return Json(e.Message);
                }
                
                return RedirectToAction("Devices", "Admin");
            }
            return View(model);
        }

        // Get: Admin/Device/Details/{deviceId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/Device/Details/{deviceId}")]
        public async Task<IActionResult> Details(int deviceId)
        {
            var result = new DeviceDetailsViewModel
            {
                Device = await _deviceService.GetDeviceByIdAsync(deviceId),
                Events = _deviceService.GetEventsForADevice(deviceId)
            };
            return View(result);
        }

        // Get: Admin/Device/Delete/{deviceId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("Admin/Device/Delete/{deviceId}")]
        public async Task<IActionResult> Delete(int deviceId)
        {
            var device = await _deviceService.GetDeviceByIdAsync(deviceId);
            return View(device);
        }

        // Post: Admin/Device/Delete/{deviceId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Admin/Device/Delete/{deviceId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int deviceId)
        {
            await _deviceService.RemoveAsync(deviceId);
            return RedirectToAction("Devices", "Admin");
        }
               
    }
}