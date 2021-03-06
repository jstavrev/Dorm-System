﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartDormitory.Models.DbModels;
using SmartDormitory.Services.Contracts;
using SmartDormitory.Web.Models;

namespace SmartDormitory.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserSensorService _userSensorService;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public HomeController(IUserSensorService userSensorService, UserManager<User> userManager, IUserService userService)
        {
            this._userSensorService = userSensorService;
            this._userManager = userManager;
            this._userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var users = await _userSensorService.GetSensorsForMapAsync();
                var newList = new List<IndexViewModel>();
                foreach (var d in users)
                {
                    var vm = new IndexViewModel
                    {
                        Latitude = d.Latitude,
                        SensorName = d.Name,
                        Longitude = d.Longitude,
                        UserMaxValue = d.UserMaxValue,
                        UserMinValue = d.UserMinValue,
                        Value = d.Value
                    };

                    var user = await this._userManager.FindByIdAsync(d.UserId);
                    vm.Owner = user.UserName;

                    newList.Add(vm);
                }

                TempData["Sensors"] = this._userSensorService.UserSensorsCount();
                TempData["Users"] = this._userService.Total();
                return View(newList);
            }
            catch
            {
                return View("PageNotFound");
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
