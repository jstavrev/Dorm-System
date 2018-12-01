﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartDormitory.Web.Areas.Administration.Controllers
{
    [Area("administration")]
    [Authorize(Roles = "Admin")]
    public class SensorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}