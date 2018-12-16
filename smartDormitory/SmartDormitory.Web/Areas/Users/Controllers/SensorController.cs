using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartDormitory.Models.DbModels;
using SmartDormitory.Services.Contracts;
using SensorValidation = SmartDormitory.Web.Areas.Administration.Models.Sensor.SensorValidationsViewModel;
using SmartDormitory.Web.Areas.Users.Models;
using X.PagedList;

namespace SmartDormitory.Web.Areas.Users.Controllers
{
    [Area("Users")]
    public class SensorController : Controller
    {
        private readonly ISensorService sensorService;
        private readonly UserManager<User> _userManager;

        public SensorController(ISensorService sensorService, UserManager<User> userManager)
        {
            this.sensorService = sensorService;
            _userManager = userManager;
        }

        [HttpGet("Users/Sensor/Register/{id?}")]
        public IActionResult Register(string userId)
        {
            var sensors = this.sensorService.GetAll().ToList();
            var sensorTypes = this.sensorService.GetAllTypes().ToList();
            var model = new RegisterSensorViewModel(sensors, sensorTypes, sensors);
            ViewBag.userId = userId;
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(RegisterSensorViewModel model)
        {
            var adminRegistration = true;

            if (model.UserID == null)
            {
                model.UserID = this._userManager.GetUserId(User);
                adminRegistration = false;
            }
            try
            {
                this.sensorService.RegisterSensor(model.Longitude, model.Latitude, model.MinValue, model.MaxValue, model.UpdateInterval, model.Name, model.Description, model.IsPublic, model.IsRequiredNotification, model.Default, model.UserID, model.SensorId);
            }
            catch
            {
                TempData["InvalidModel"] = "Minimum value cannot be bigger than maximum value, please try again!";
                if (adminRegistration)
                {
                    return RedirectToRoute(new
                    {
                        controller = "Sensor",
                        action = "Register",
                        userId = model.UserID,
                    });
                }

                return RedirectToRoute(new
                {
                    controller = "Sensor",
                    action = "Register"
                });
            }            

            if (model.UserID != this._userManager.GetUserId(User))
            {
                return RedirectToAction("Index", "Sensor", new { area = "Administration" });
            }

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

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public IActionResult EditValidationView(int typeId)
        {
            if (typeId == 4)
            {
                return PartialView("_TrueFalseEditValidationView");
            }
            return PartialView("_MinMaxEditValidationView");
        }

        [HttpGet("Users/Sensor/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var sensor = await sensorService.FindAsync(id);
            var sensorType = sensorService.Find(sensor.SensorId);

            if (sensor == null)
            {
                throw new ApplicationException($"Unable to find sensor with ID '{id}'.");
            }
            var sensorValidation = new SensorValidation(sensorType);
            var model = new SensorEditViewModel(sensor,sensorValidation);

            return PartialView(model);
        }


        [HttpPost("Users/Sensor/Edit/{id}")]
        [Authorize]
        public async Task<IActionResult> Edit(SensorEditViewModel model)
        {
            var sensor = await sensorService.FindAsync(model.Id);
            if (sensor == null)
            {
                throw new ApplicationException($"Unable to find sensor with ID '{model.Id}'.");
            }

            if (sensor.Longitude !=model.Longitude || sensor.Latitude != model.Latitude)
            {
                await sensorService.ChangeCoordinatesAsync(model.Id, model.Longitude, model.Latitude);
            }

            if (sensor.UserMinValue != model.MinValue || sensor.UserMaxValue != model.MaxValue)
            {
                await sensorService.ChangeMinMaxAsync(model.Id, model.MinValue,model.MaxValue);
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

            return View("Index", model);
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
                .Select(s => new CreateDashboardSensorSelectionViewModel { Id = s.Id, Name = s.Name, Description = s.Description, Type = s.Type }).ToList());

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
                        new DashboardSensorViewModel
                        {
                            Description = uS.Description,
                            Id = uS.Id,
                            MaxValue = uS.MaxValue,
                            MinValue = uS.MinValue,
                            Name = uS.Name,
                            Value = uS.Value,
                            UpdateInterval = uS.UpdateInterval,
                            UserMaxValue = uS.UserMaxValue,
                            UserMinValue = uS.UserMinValue,
                            LastUpdate = uS.LastUpdatedOn,
                            DefaultPosition = uS.UserMaxValue
                        };
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

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public IActionResult UpdateDashboard(string ids)
        {
            List<string> idsList = ids.Split(',').ToList();

            List<UpdateDashboardViewModel> updatedSensors = new List<UpdateDashboardViewModel>();

            for (int i = 0; i < idsList.Count; i++)
            {
                if (idsList[i] != "")
                {
                    var userSensor = this.sensorService.GetUserSensorsById(int.Parse(idsList[i]));
                    var updatedSensor = new UpdateDashboardViewModel
                    {
                        Value = userSensor.Value,
                        Id = userSensor.Id,
                        LastUpdate = userSensor.LastUpdatedOn
                    };
                    updatedSensors.Add(updatedSensor);
                }
            }

            return Json(updatedSensors);
        }
    }
}