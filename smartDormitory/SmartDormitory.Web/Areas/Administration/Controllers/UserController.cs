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
    public class UserController : Controller
    {
        private const int pageSize = 10;
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index(UserIndexViewModel model)
        {
            if (model.SearchText == null)
            {
                model.TotalPages = (int)Math.Ceiling(this.userService.Total() / (double)pageSize);
                model.users = this.userService.GetUsersWithPaging(model.Page, pageSize);
            }
            else
            {
                model.TotalPages = (int)Math.Ceiling(this.userService.TotalContainingText(model.SearchText) / (double)pageSize);
                model.users = this.userService.GetUsersContainingText(model.SearchText, model.Page, pageSize);
            }

            return View(model);
        }
    }
}