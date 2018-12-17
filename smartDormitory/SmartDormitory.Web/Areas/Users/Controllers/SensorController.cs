using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartDormitory.Models.DbModels;
using SmartDormitory.Services.Contracts;
using SensorValidation = SmartDormitory.Web.Areas.Administration.Models.Sensors.SensorValidationsViewModel;
using SmartDormitory.Web.Areas.Users.Models;
using X.PagedList;

namespace SmartDormitory.Web.Areas.Users.Controllers
{
    [Area("Users")]
    [Authorize]
    public class SensorController : Controller
    {
        private readonly IUserSensorService _userSensorService;
        private readonly ISensorService _sensorService;
        private readonly UserManager<User> _userManager;

        public SensorController(IUserSensorService userSensorService, UserManager<User> userManager, ISensorService sensorService)
        {
            this._userSensorService = userSensorService;
            _userManager = userManager;
            this._sensorService = sensorService;
        }

        [HttpGet("Users/Sensor/Register/{id?}")]
        [Authorize]
        public IActionResult Register(string userId)
        {
            try
            {
                var sensors = this._sensorService.GetAll().ToList();
                var sensorTypes = this._sensorService.GetAllTypes().ToList();
                var model = new RegisterSensorViewModel(sensors, sensorTypes, sensors);
                ViewBag.userId = userId;
                return View(model);
            }
            catch
            {
                return View("PageNotFound");
            }
        }

        [HttpPost]
        [Authorize]
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
                this._userSensorService.RegisterSensor(model.Longitude, model.Latitude, model.MinValue, model.MaxValue, model.UpdateInterval, model.Name, model.Description, model.IsPublic, model.IsRequiredNotification, model.Default, model.UserID, model.SensorId);
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

        [HttpGet("Sensors")]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var user = _userManager.GetUserId(User);
                var sensors = await _userSensorService.FilterUserSensorsAsync(user);

                var model = new SensorDetailsViewModel(sensors);

                return View(model);
            }
            catch
            {
                return View("PageNotFound");
            }
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        [Authorize]
        public IActionResult EditValidationView(int typeId)
        {
            try
            {
                if (typeId == 4)
                {
                    return PartialView("_TrueFalseEditValidationView");
                }
                return PartialView("_MinMaxEditValidationView");
            }
            catch
            {
                return View("PageNotFound");
            }
        }

        [HttpGet("Users/Sensor/Edit/{id}")]
        [Authorize]
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
                var sensorValidation = new SensorValidation(sensorType);
                var model = new SensorEditViewModel(sensor, sensorValidation);

                return PartialView(model);
            }
            catch
            {
                return View("PageNotFound");
            }
        }


        [HttpPost("Users/Sensor/Edit/{id}")]
        [Authorize]
        public async Task<IActionResult> Edit(SensorEditViewModel model)
        {
            try
            {
                var sensor = await _userSensorService.FindAsync(model.Id);
                if (sensor == null)
                {
                    throw new ApplicationException($"Unable to find sensor with ID '{model.Id}'.");
                }

                if (sensor.Longitude != model.Longitude || sensor.Latitude != model.Latitude)
                {
                    await _userSensorService.ChangeCoordinatesAsync(model.Id, model.Longitude, model.Latitude);
                }

                if (sensor.UserMinValue != model.MinValue || sensor.UserMaxValue != model.MaxValue)
                {
                    await _userSensorService.ChangeMinMaxAsync(model.Id, model.MinValue, model.MaxValue);
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

                if (sensor.Description != model.Description)
                {
                    await _userSensorService.ChangeDescriptionAsync(model.Id, model.Description);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("PageNotFound");
            }
        }

        [HttpGet("Users/Sensor/showmap/{id}")]
        [Authorize]
        public async Task<IActionResult> ShowMap(int id)
        {
            try
            {
                var sensor = await _userSensorService.FindAsync(id);
                if (sensor == null)
                {
                    throw new ApplicationException($"Unable to find sensor with ID '{id}'.");
                }

                var model = new SensorMapViewModel(sensor);
                return PartialView(model);
            }
            catch
            {
                return View("PageNotFound");
            }
        }

        [HttpGet("Sensors/Filter")]
        [Authorize]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            try
            {
                sortOrder = sortOrder ?? string.Empty;
                searchTerm = searchTerm ?? string.Empty;

                var user = _userManager.GetUserId(User);
                var sensors = await _userSensorService.FilterUserSensorsAsync(user, sortOrder, searchTerm, pageNumber ?? 1, pageSize ?? 10);

                var model = new SensorDetailsViewModel(sensors);

                return View("Index", model);
            }
            catch
            {
                return View("PageNotFound");
            }
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        [Authorize]
        public IActionResult SensorSelect(int typeId)
        {
            try
            {
                var result = new List<SensorSelectViewModel>();

                if (typeId == 0)
                {
                    result = this._sensorService.GetAll().Select(s => new SensorSelectViewModel { Id = s.Id, Name = s.Name }).ToList();
                }
                else
                {
                    result = this._sensorService.GetAll().Where(s => s.SensorTypeId == typeId).Select(s => new SensorSelectViewModel { Id = s.Id, Name = s.Name }).ToList();
                }

                return Json(result);
            }
            catch
            {
                return View("PageNotFound");
            }
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        [Authorize]
        public IActionResult SensorValidationInfo(int sensorId)
        {
            try
            {
                var sensor = this._sensorService.Find(sensorId);
                var sensorType = this._sensorService.GetAllTypes().Where(s => s.Id == sensor.SensorTypeId).FirstOrDefault();
                var result = new SensorValidationViewModel
                {
                    MaxValue = sensor.MaxValue,
                    MinValue = sensor.MinValue,
                    UpdateInterval = sensor.MinPollingIntervalInSeconds,
                    Type = sensorType.Type
                };

                return Json(result);
            }
            catch
            {
                return View("PageNotFound");
            }
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        [Authorize]
        public IActionResult ValidationView(string type)
        {
            try
            {
                if (type == "(true/false)")
                {
                    return PartialView("_TrueFalseValidationView");
                }
                return PartialView("_MinMaxValidationView");
            }
            catch
            {
                return View("PageNotFound");
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateDashboard()
        {
            try
            {
                var userId = _userManager.GetUserId(User);

                CreateDashboardViewModel model = new CreateDashboardViewModel(this._userSensorService.GetAllUserSensorsByUser(userId)
                    .Select(s => new CreateDashboardSensorSelectionViewModel { Id = s.Id, Name = s.Name, Description = s.Description, Type = s.Type }).ToList());

                return View(model);
            }
            catch
            {
                return View("PageNotFound");
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateDashboard(CreateDashboardViewModel model)
        {
            try
            {
                var userId = _userManager.GetUserId(User);

                var selectedDashboardSensors = new List<DashboardSensorViewModel>();
                var allUserSensorsDictionary = this._userSensorService.GetAllUserSensorsByUserDictionary(userId);

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
            catch
            {
                return View("PageNotFound");
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Dashboard(DashboardViewModel model)
        {
            try
            {
                model = TempData.Get<DashboardViewModel>("dashboard");

                if (model == null || model.IsEmpty)
                {
                    return RedirectToAction("CreateDashboard", "Sensor");
                }

                return View(model);
            }
            catch
            {
                return View("PageNotFound");
            }
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        [Authorize]
        public IActionResult UpdateDashboard(string ids)
        {
            try
            {
                List<string> idsList = ids.Split(',').ToList();

                List<UpdateDashboardViewModel> updatedSensors = new List<UpdateDashboardViewModel>();

                for (int i = 0; i < idsList.Count; i++)
                {
                    if (idsList[i] != "")
                    {
                        var userSensor = this._userSensorService.GetUserSensorsById(int.Parse(idsList[i]));
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
            catch
            {
                return View("PageNotFound");
            }
        }
    }
}