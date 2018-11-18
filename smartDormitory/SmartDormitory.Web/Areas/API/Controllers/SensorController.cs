using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartDormitory.Services.Services;
using SmartDormitory.Web.Areas.API.Models;

namespace SmartDormitory.Web.Areas.API.Controllers
{
    [Area("API")]
    public class SensorController : Controller
    {
        private ApiService apiService;

        public SensorController(ApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<IActionResult> All()
        {
            var sensors = await this.apiService.GetAll();
            var viewModel = new AllSensorsViewModel(sensors);

            return View(viewModel);
        }
    }
}