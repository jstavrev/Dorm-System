using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartDormitory.Models.DbModels;
using SmartDormitory.Services.Contracts;
using SmartDormitory.Web.Areas.Administration.Models.Sensors;

namespace SmartDormitory.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class SensorController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserSensorService _userSensorService;
        private readonly ISensorService _sensorService;

        public SensorController(IUserSensorService userSensorService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, ISensorService sensorService)
        {
            this._userSensorService = userSensorService ?? throw new ArgumentNullException(nameof(userSensorService));
            this._roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            this._sensorService = sensorService;
        }

        [HttpGet("Administration/Sensors")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var usersensors = await _userSensorService.FilterAllSensorsAsync();
                var model = new SensorIndexViewModel(usersensors);

                return View(model);
            }
            catch
            {
                return View("PageNotFound");
            }
        }

        [HttpGet("administration/sensor/filter")]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            try
            {
                sortOrder = sortOrder ?? string.Empty;
                searchTerm = searchTerm ?? string.Empty;

                var users = await _userSensorService.FilterAllSensorsAsync(sortOrder, searchTerm, pageNumber ?? 1, pageSize ?? 10);
                var model = new SensorIndexViewModel(users);

                return View("Index", model);
            }
            catch
            {
                return View("PageNotFound");
            }
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public IActionResult AdminValidationView(int typeId)
        {
            try
            {
                if (typeId == 4)
                {
                    return PartialView("_TrueFalseAdminValidationView");
                }

                return PartialView("_MinMaxAdminValidationView");
            }
            catch
            {
                return View("PageNotFound");
            }
        }

        [HttpGet("Administration/Sensors/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var sensor = await _userSensorService.FindAsync(id);

                var sensorType = _sensorService.Find(sensor.SensorId);

                if (sensor == null)
                {
                    throw new ApplicationException($"Unable to find sensor with ID '{id}'.");
                }
                var sensorValidation = new SensorValidationsViewModel(sensorType);

                var model = new SensorDetailsViewModel(sensor, sensorValidation);

                return View(model);
            }
            catch
            {
                return View("PageNotFound");
            }
        }

        [HttpPost("Administration/Sensors/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SensorDetailsViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var sensor = await _userSensorService.FindAsync(model.Id);
                if (sensor == null)
                {
                    throw new ApplicationException($"Unable to find sensor with ID '{model.Id}'.");
                }

                if (sensor.Longitude != model.Longitude || sensor.Latitude != model.Latitude)
                {
                    await _userSensorService.ChangeCoordinatesAsync(model.Id, model.Longitude, model.Latitude);
                }

                if (model.Default == null)
                {
                    if (sensor.UserMinValue != model.MinValue || sensor.UserMaxValue != model.MaxValue)
                    {
                        await _userSensorService.ChangeMinMaxAsync(model.Id, model.MinValue, model.MaxValue);
                    }
                }
                else
                {
                    if (sensor.UserMinValue != int.Parse(model.Default))
                    {
                        await _userSensorService.ChangeMinMaxAsync(model.Id, int.Parse(model.Default), int.Parse(model.Default));
                    }
                }

                if (sensor.IsPublic != model.IsPublic)
                {
                    await _userSensorService.ChangeIsPublicAsync(model.Id, model.IsPublic);
                }

                if (sensor.IsRequiredNotification != model.IsRequiredNotification)
                {
                    await _userSensorService.ChangeIsRequiredNotificationAsync(model.Id, model.IsRequiredNotification);
                }

                if (sensor.Name != model.Name)
                {
                    await _userSensorService.ChangeNameAsync(model.Id, model.Name);
                }

                if (sensor.UpdateInterval != model.UpdateInterval)
                {
                    await _userSensorService.ChangeUpdatenIntervalAsync(model.Id, model.UpdateInterval);
                }

                if (sensor.Description != model.Description)
                {
                    await _userSensorService.ChangeDescriptionAsync(model.Id, model.Description);
                }

                var usersensors = await _userSensorService.FilterAllSensorsAsync();

                var newModel = new SensorIndexViewModel(usersensors);

                return View("Index", newModel);
            }
            catch
            {
                return View("PageNotFound");
            }
        }

    }
}