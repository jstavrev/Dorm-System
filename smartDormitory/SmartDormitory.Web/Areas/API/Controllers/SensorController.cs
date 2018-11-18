using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SmartDormitory.Web.Areas.API.Controllers
{
    public class SensorController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}