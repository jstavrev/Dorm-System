using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SmartDormitory.Web.Areas.Users.Controllers
{
    [Area("users")]
    public class SensorController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult MySensors()
        {
            return View();
        }
    }
}