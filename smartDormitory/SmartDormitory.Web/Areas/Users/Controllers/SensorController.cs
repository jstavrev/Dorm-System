﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public SensorController(ISensorService sensorService,  UserManager<User> userManager)
        {
            this.sensorService = sensorService;
            _userManager = userManager;
        }

        public IActionResult Register()
        {
            var sensors = this.sensorService.GetAll().ToList();
            var sensorTypes = this.sensorService.GetAllTypes().ToList();
            var model = new RegisterSensorViewModel(sensors, sensorTypes, sensors);

            return View(model);
        }

        [HttpPost]
        public IActionResult Register(RegisterSensorViewModel model)
        {
            string userId = this._userManager.GetUserId(User);
            this.sensorService.RegisterSensor(model.Longitude, model.Latitude, model.MinValue, model.MaxValue, model.UpdateInterval, model.Name, model.Description, model.IsPublic, model.IsRequiredNotification, model.Default, userId, model.SensorId);

            return RedirectToAction("Index", "Sensor");
        }

        public IActionResult MySensors()
        {
            return View();
        }

        [HttpGet("Sensors")]
        public async Task<IActionResult> Index()
        {

            var user = _userManager.GetUserId(User);
            var sensors = await sensorService.FilterUserSensorsAsync(user);

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

            if (sensor.Longitude != double.Parse(model.Longitude) || sensor.Latitude != double.Parse(model.Latitude))
            {
                await sensorService.ChangeCoordinatesAsync(model.Id, double.Parse(model.Longitude), double.Parse(model.Latitude));
            }

            if (sensor.MinValue != model.MinValue || sensor.MaxValue != model.MaxValue)
            {
                await sensorService.ChangeMinMaxAsync(model.Id, model.MinValue, model.MaxValue);
            }

            if (sensor.IsPublic != model.IsPublic)
            {
                await sensorService.ChangeIsPublicAsync(model.Id, model.IsPublic);
            }

            if (sensor.IsRequiredNotification != model.IsRequiredNotification)
            {
                await sensorService.ChangeIsRequiredNotificationAsync(model.Id, model.IsRequiredNotification);
            }

            if (sensor.Name != model.Name)
            {
                await sensorService.ChangeNameAsync(model.Id, model.Name);
            }

            if (sensor.Description != model.Description)
            {
                await sensorService.ChangeDescriptionAsync(model.Id, model.Description);
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

       [HttpGet("Sensors/Filter")]
       public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
       {
           sortOrder = sortOrder ?? string.Empty;
           searchTerm = searchTerm ?? string.Empty;

            var user = _userManager.GetUserId(User);
            var sensors = await sensorService.FilterUserSensorsAsync(user, sortOrder, searchTerm, pageNumber ?? 1, pageSize ?? 10);

            var model = new SensorDetailsViewModel(sensors);

            return View("Index",model);
       }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public IActionResult SensorSelect(int typeId)
        {
            var result = new List<SensorSelectViewModel>();

            if (typeId == 0)
            {
                result = this.sensorService.GetAll().Select(s => new SensorSelectViewModel { Id = s.Id, Name = s.Name }).ToList();
            }
            else
            {
                result = this.sensorService.GetAll().Where(s => s.SensorTypeId == typeId).Select(s => new SensorSelectViewModel { Id = s.Id, Name = s.Name }).ToList();
            }

            return Json(result);
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public IActionResult SensorValidationInfo(int sensorId)
        {
            var sensor = this.sensorService.Find(sensorId);
            var sensorType = this.sensorService.GetAllTypes().Where(s => s.Id == sensor.SensorTypeId).FirstOrDefault();
            var result = new SensorValidationViewModel
            {
                MaxValue = sensor.MaxValue,
                MinValue = sensor.MinValue,
                UpdateInterval = sensor.MinPollingIntervalInSeconds,
                Type = sensorType.Type
            };

            return Json(result);
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public IActionResult ValidationView(string type)
        {
            if (type == "(true/false)")
            {
                return PartialView("_TrueFalseValidationView");
            }
            return PartialView("_MinMaxValidationView");
        }
    }
}