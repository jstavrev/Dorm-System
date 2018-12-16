using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartDormitory.Data.Services.Contracts;
using SmartDormitory.Models.DbModels;
using SmartDormitory.Web.Models;

namespace SmartDormitory.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        private readonly UserManager<User> _userManager;

        public HomeController(IHomeService homeService, UserManager<User> userManager)
        {
            _homeService = homeService ?? throw new ArgumentNullException(nameof(homeService));
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
           var users = await _homeService.FilterSensorsAsync();
            var newList = new List<IndexViewModel>();
            foreach (var d in users)
            {
                var vm = new IndexViewModel {
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

            return View(newList);
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
