using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartDormitory.Services.Contracts;
using SmartDormitory.Web.Areas.Administration.Models;

namespace SmartDormitory.Web.Areas.Administration.Controllers
{
    [Area("administration")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IApiService ApiService;

        public AdminController(IApiService ApiService)
        {
            this.ApiService = ApiService;
        }

        public async Task<IActionResult> Index()
        {
            var sensors = await this.ApiService.GetAll();
            var viewModel = new AdminIndexViewModel(sensors);

            return View(viewModel);
        }
    }
}