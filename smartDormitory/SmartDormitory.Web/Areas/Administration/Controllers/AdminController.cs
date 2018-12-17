using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartDormitory.Services.Contracts;
using SmartDormitory.Web.Areas.Administration.Models;

namespace SmartDormitory.Web.Areas.Administration.Controllers
{
    [Area("administration")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ISensorService SensorService;

        public AdminController(ISensorService sensorService)
        {
            this.SensorService = sensorService;
        }

        public IActionResult Index()
        {
            try
            {
                var sensors = this.SensorService.GetAll().ToList();
                var viewModel = new AdminIndexViewModel(sensors);
                return View(viewModel);
            }
            catch
            {
                return View("PageNotFound");
            }
        }
    }
}