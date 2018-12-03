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
    [Area("users")]
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
    }
}