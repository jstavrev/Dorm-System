using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartDormitory.Data.Services.Contracts;
using SmartDormitory.Web.Models;

namespace SmartDormitory.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService ?? throw new ArgumentNullException(nameof(homeService));
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
                    Longitude = d.Longitude
                };
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
