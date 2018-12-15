using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartDormitory.Models.DbModels;
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

        [HttpGet("Administration/Sensors")]
        public async Task<IActionResult> Index()
        {
            var usersensors = await _sensorService.FilterAllSensorsAsync();
            var model = new SensorIndexViewModel(usersensors);

            return View(model);
        }

        [HttpGet("administration/sensor/filter")]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            sortOrder = sortOrder ?? string.Empty;
            searchTerm = searchTerm ?? string.Empty;

            var users = await _sensorService.FilterAllSensorsAsync(sortOrder, searchTerm, pageNumber ?? 1, pageSize ?? 10);
            var model = new SensorIndexViewModel(users);

            return View("Index", model);
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public IActionResult AdminValidationView(int typeId)
        {
            if (typeId == 4)
            {
                return PartialView("_TrueFalseAdminValidationView");
            }
            return PartialView("_MinMaxAdminValidationView");
        }

        [HttpGet("Administration/Sensors/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var sensor = await _sensorService.FindAsync(id);

            var sensorType = _sensorService.Find(sensor.SensorId);

            if (sensor == null)
            {
                throw new ApplicationException($"Unable to find sensor with ID '{id}'.");
            }
            var sensorValidation = new SensorValidationsViewModel(sensorType);

            var model = new SensorDetailsViewModel(sensor, sensorValidation);

            return View(model);
        }

        [HttpPost("Administration/Sensors/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SensorDetailsViewModel model)
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

            if (sensor.Longitude != model.Longitude || sensor.Latitude != model.Latitude)
            {
                await _sensorService.ChangeCoordinatesAsync(model.Id, model.Longitude, model.Latitude);
            }

            if (model.Default == null)
            {
                if (sensor.UserMinValue != model.MinValue || sensor.UserMaxValue != model.MaxValue)
                {
                    await _sensorService.ChangeMinMaxAsync(model.Id, model.MinValue, model.MaxValue);
                }
            }
            else
            {
                if (sensor.UserMinValue != int.Parse(model.Default))
                {
                    await _sensorService.ChangeMinMaxAsync(model.Id, int.Parse(model.Default), int.Parse(model.Default));
                }
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

            var usersensors = await _sensorService.FilterAllSensorsAsync();

            var newModel = new SensorIndexViewModel(usersensors);

            return View("Index", newModel);
        }

        [HttpGet("Administration/Users/Registersensor/{sensorid}/{userid}")]
        [ResponseCache(CacheProfileName = "Short")]
        public IActionResult Register(int sensorId, string userId)
        {
            ViewBag.sensorId = sensorId;
            ViewBag.userId = userId;
            return View("RegisterSensor");
        }

        [HttpPost("Administration/Users/Registersensor/{sensorid}/{userid}")]
        public IActionResult Register(RegisterSensorViewModel model)
        {
            _sensorService.RegisterSensor(model.Longitude, model.Latitude, model.MinValue, model.MaxValue, model.UpdateInterval, model.Name, model.Description,
                model.IsPublic, model.IsRequiredNotification, "2", model.UserId, model.SensorId.ToString());

            return RedirectToAction("Index", "Sensor");
        }
    }
}