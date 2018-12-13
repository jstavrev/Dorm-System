using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartDormitory.Models.DbModels;
using SmartDormitory.Services.Contracts;
using SmartDormitory.Web.Areas.Administration.Models;

namespace SmartDormitory.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISensorService sensorService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public UserController(IUserService userService, ISensorService sensorService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.sensorService = sensorService ?? throw new ArgumentNullException(nameof(sensorService));

            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpGet("Administration/Users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.FilterUsersAsync();
            var sensors = sensorService.GetAll().ToList();
            var userModel = new UserIndexViewModel(users);
            var model = new UserTableViewModel(sensors,sensors, userModel);
            return View(model);
        }

        [HttpGet("Administration/Users/Details/{id}")]
        [ResponseCache(CacheProfileName = "Short")]
        public async Task<IActionResult> Details(string id)
        {
            const string adminRole = "Admin";

            if (id == null)
            {
                throw new ApplicationException($"Passed ID parameter is absent.");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new ApplicationException($"Unable to find user with ID '{id}'.");
            }

            var model = new UserDetailsViewModel(user, await _userManager.IsInRoleAsync(user, adminRole));

            return View(model);
        }

        [HttpPost("Administration/Users/Details/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(UserDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{model.Id}'.");
            }

            var email = user.Email;
            if (model.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            var userName = user.UserName;
            if (model.Username != userName)
            {
                var setUserNameResult = await _userManager.SetEmailAsync(user, model.Username);
                if (!setUserNameResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting UserName for user with ID '{user.Id}'.");
                }
            }

            var firstName = user.FirstName;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Administration/Users/Filter")]
        public async Task<IActionResult> Filter(string sortOrder, string searchTerm, int? pageSize, int? pageNumber)
        {
            sortOrder = sortOrder ?? string.Empty;
            searchTerm = searchTerm ?? string.Empty;

            var users = await _userService.FilterUsersAsync(sortOrder, searchTerm, pageNumber ?? 1, pageSize ?? 10);
            var sensors = sensorService.GetAll().ToList();
            var userModel = new UserIndexViewModel(users);
            var model = new UserTableViewModel(sensors, sensors, userModel);
            return View("Index",model);
        }

        [HttpGet("Administration/Users/Promote/{id}")]
        public async Task<IActionResult> Promote(string id)
        {
            const string adminRole = "Admin";

            if (!await _roleManager.RoleExistsAsync(adminRole))
            {
                throw new ApplicationException(string.Format("User promotion unsuccessful , {0} role does not exists.", adminRole));
            }

            var user = await _userManager.FindByIdAsync(id);

            var addRoleResult = await _userManager.AddToRoleAsync(user, adminRole);
            if (!addRoleResult.Succeeded)
            {
                throw new ApplicationException(string.Format("User promotion operation was unsuccessful."));
            }

            var model = new UserDetailsViewModel(user, await _userManager.IsInRoleAsync(user, adminRole));

            return View("Details", model);
        }

        [HttpGet("Administration/Users/Demote/{id}")]
        public async Task<IActionResult> Demote(string id)
        {
            const string adminRole = "Admin";

            if (!await _roleManager.RoleExistsAsync(adminRole))
            {
                throw new ApplicationException(string.Format("User demotion unsuccessful , {0} role does not exists.", adminRole));
            }

            var user = await _userManager.FindByIdAsync(id);

            var removeRoleResult = await _userManager.RemoveFromRoleAsync(user, adminRole);
            if (!removeRoleResult.Succeeded)
            {
                throw new ApplicationException(string.Format("User demotion operation was unsuccessful."));
            }

            var model = new UserDetailsViewModel(user, await _userManager.IsInRoleAsync(user, adminRole));

            return View("Details", model);
        }
    }
}