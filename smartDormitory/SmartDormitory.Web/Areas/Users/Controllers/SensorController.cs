using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartDormitory.Services.Contracts;
using SmartDormitory.Web.Areas.Users.Models;

namespace SmartDormitory.Web.Areas.Users.Controllers
{
    [Area("users")]
    public class SensorController : Controller
    {
        private readonly ISensorService sensorService;

        public SensorController(ISensorService sensorService)
        {
            this.sensorService = sensorService;
        }

        public IActionResult Register()
        {
            var sensors = this.sensorService.GetAll().ToList();
            var sensorTypes = this.sensorService.GetAllTypes().ToList();
            var model = new RegisterSensorViewModel(sensors, sensorTypes);

            return View(model);
        }

        [HttpPost]
        public IActionResult Register(RegisterSensorViewModel model)
        {
            return View();
        }

        public IActionResult MySensors()
        {
            return View();
        }
    }
}