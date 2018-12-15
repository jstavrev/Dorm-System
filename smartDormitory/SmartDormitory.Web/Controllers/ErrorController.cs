using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartDormitory.Data.Services.Contracts;
using SmartDormitory.Web.Models;

namespace SmartDormitory.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            var errorModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier
            };
                
            return this.View(errorModel);
        }

        public IActionResult PageNotFound()
        {
            return this.View();
        }
    }
}
