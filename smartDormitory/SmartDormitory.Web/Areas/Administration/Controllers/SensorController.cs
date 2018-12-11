using System;
using System.Threading.Tasks;
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

        public SensorController(ISensorService sensorService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _sensorService = sensorService ?? throw new ArgumentNullException(nameof(sensorService));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        [HttpGet("usersensors")]
        public async Task<IActionResult> Index()
        {
            var usersensors = await _sensorService.FilterAllSensorsAsync();

            var model = new SensorIndexViewModel(usersensors);

            return View(model);
        }

        [HttpGet("sensors/details/{id}")]
        [ResponseCache(CacheProfileName = "Short")]
        public async Task<IActionResult> Details(int id)
        {

            var sensor = await _sensorService.FindAsync(id);
            if (sensor == null)
            {
                throw new ApplicationException($"Unable to find sensor with ID '{id}'.");
            }

            var model = new SensorDetailsViewModel(sensor);

            return View(model);
        }

        [HttpPost("sensors/details/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(SensorEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var sensor = await _sensorService.FindAsync(model.Id);
            if (sensor == null)
            {
                throw new ApplicationException($"Unable to find sensor with ID '{model.Id}'.");
            }

            if (sensor.Longitude != model.Longitude || sensor.Latitude !=model.Latitude)
            {
                await _sensorService.ChangeCoordinatesAsync(model.Id, model.Longitude, model.Latitude);
            }

            if (sensor.MinValue != model.MinValue || sensor.MaxValue != model.MaxValue)
            {
                await _sensorService.ChangeMinMaxAsync(model.Id, model.MinValue, model.MaxValue);
            }

            if (sensor.IsPublic != model.IsPublic)
            {
                await _sensorService.ChangeIsPublicAsync(model.Id, model.IsPublic);
            }

            if (sensor.IsRequiredNotification != model.IsRequiredNotification)
            {
                await _sensorService.ChangeIsRequiredNotificationAsync(model.Id, model.IsRequiredNotification);
            }

            if (sensor.Name != model.Name)
            {
                await _sensorService.ChangeNameAsync(model.Id, model.Name);
            }

            if (sensor.UpdateInterval != model.UpdateInterval)
            {
                await _sensorService.ChangeUpdatenIntervalAsync(model.Id, model.UpdateInterval);
            }

            return RedirectToAction(nameof(Details));
        }
    }
}