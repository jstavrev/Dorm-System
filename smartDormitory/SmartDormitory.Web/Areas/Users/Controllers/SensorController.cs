using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SmartDormitory.Data.Data;
using SmartDormitory.Models.DbModels;
using SmartDormitory.Services.Contracts;
using SmartDormitory.Web.Areas.Users.Models;
using X.PagedList;

namespace SmartDormitory.Web.Areas.Users.Controllers
{
    [Area("Users")]
    public class SensorController : Controller
    {
        private readonly ISensorService sensorService;
        private readonly UserManager<User> _userManager;
        private readonly IMemoryCache _memoryCache;

        public SensorController(ISensorService sensorService, IMemoryCache memoryCache, UserManager<User> userManager)
        {
            this.sensorService = sensorService;
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _userManager = userManager;
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

        [HttpGet("sensors")]
        public async Task<IActionResult> Index()
        {
            if (!_memoryCache.TryGetValue("ListOfSensors", out IPagedList<UserSensors> sensors))
            {
                var user = _userManager.GetUserId(User);
                sensors = await sensorService.FilterUserSensorsAsync(user);
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(25),
                    SlidingExpiration = TimeSpan.FromSeconds(5)
                };

                _memoryCache.Set("ListOfSensors", sensors, options);
            }

            var model = new SensorDetailsViewModel(sensors);

            return View(model);
        }

        [HttpGet("Users/Sensor/sensoredit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var sensor = await sensorService.FindAsync(id);
            if (sensor == null)
            {
                throw new ApplicationException($"Unable to find sensor with ID '{id}'.");
            }

            var model = new SensorEditViewModel(sensor);

            return PartialView(model);
        }


        [HttpPost("Users/Sensor/sensoredit/{id}")]
        public async Task<IActionResult> Edit(SensorEditViewModel model)
        {
            var sensor = await sensorService.FindAsync(model.Id);
            if (sensor == null)
            {
                throw new ApplicationException($"Unable to find sensor with ID '{model.Id}'.");
            }

            if (sensor.Longitude != model.Longitude || sensor.Latitude != model.Latitude)
            {
                await sensorService.ChangeCoordinatesAsync(model.Id, model.Longitude, model.Latitude);
            }

            if (sensor.MinValue != model.MinValue || sensor.MaxValue != model.MaxValue)
            {
                await sensorService.ChangeMinMaxAsync(model.Id, model.MinValue, model.MaxValue);
            }

            if (sensor.IsPublic != model.IsPublic)
            {
                await sensorService.ChangeIsPublic(model.Id, model.IsPublic);
            }

            if (sensor.IsRequiredNotification != model.IsRequiredNotification)
            {
                await sensorService.ChangeIsRequiredNotification(model.Id, model.IsRequiredNotification);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Users/Sensor/showmap/{id}")]
        public async Task<IActionResult> ShowMap(int id)
        {
            var sensor = await sensorService.FindAsync(id);
            if (sensor == null)
            {
                throw new ApplicationException($"Unable to find sensor with ID '{id}'.");
            }

            var model = new SensorMapViewModel(sensor);
            return PartialView(model);
        }
    }
}