﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        private int pageSize = 10;
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
        [Authorize]
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

        [HttpGet]
        public IActionResult CreateDashboard()
        {
            var userId = _userManager.GetUserId(User);

            CreateDashboardViewModel model = new CreateDashboardViewModel(this.sensorService.GetAllUserSensorsByUser(userId)
                .Select(s => new CreateDashboardSensorSelectionViewModel { Id = s.Id, Name = s.Name }).ToList());

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateDashboard(CreateDashboardViewModel model)
        {
            var userId = _userManager.GetUserId(User);

            var selectedDashboardSensors = new List<DashboardSensorViewModel>();
            var allUserSensorsDictionary = this.sensorService.GetAllUserSensorsByUserDictionary(userId);

            foreach (var userSensor in model.SensorSelection)
            {
                if (userSensor.IsSelected)
                {
                    var uS = allUserSensorsDictionary[userSensor.Id];
                    var dashboardSensor = 
                        new DashboardSensorViewModel { Description = uS.Description, Id = uS.Id, MaxValue = uS.MaxValue, MinValue = uS.MinValue,
                            Name = uS.Name, Value = uS.Value, UpdateInterval = uS.UpdateInterval, UserMaxValue = uS.UserMaxValue, UserMinValue = uS.UserMinValue};
                    dashboardSensor.GraphicalId = userSensor.GraphicalRepresentationId;

                    selectedDashboardSensors.Add(dashboardSensor);
                }
            }

            var dashboardModel = new DashboardViewModel(selectedDashboardSensors);

            TempData.Put("dashboard", dashboardModel);

            return RedirectToAction("Dashboard", "Sensor");
        }

        [HttpGet]
        public IActionResult Dashboard(DashboardViewModel model)
        {
            model = TempData.Get<DashboardViewModel>("dashboard");

            if (model == null || model.IsEmpty)
            {
                return RedirectToAction("CreateDashboard", "Sensor");
            }

            return View(model);
        }
    }
}