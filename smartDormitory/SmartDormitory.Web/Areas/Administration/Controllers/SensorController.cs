using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartDormitory.Data.Data;
using SmartDormitory.Services.Contracts;
using SmartDormitory.Web.Areas.Administration.Models.Sensor;

namespace SmartDormitory.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class SensorController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISensorService _sensorService;
        private readonly UserManager<User> _userManager;

        public SensorController(ISensorService sensorService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _sensorService = sensorService ?? throw new ArgumentNullException(nameof(sensorService));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpGet("usersensors")]
        public async Task<IActionResult> Index()
        {
            var usersensors = await _sensorService.FilterAllSensorsAsync();

            var model = new SensorIndexViewModel(usersensors);

            return View(model);
        }
    }
}