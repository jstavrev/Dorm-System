﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartDormitory.Models.DbModels;
using SmartDormitory.Services.Contracts;
using SmartDormitory.Web.Areas.Users.Models;
using SmartDormitory.Web.Utilities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Areas.Identity.Controllers
{
    [Authorize]
    [Area("Users")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public UserController(
          UserManager<User> userManager,
          SignInManager<User> signInManager,
          IUserService userService,
          ILogger<UserController> logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet("Profile")]
        [ResponseCache(CacheProfileName = "Short")]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                var model = new ManageViewModel(user, StatusMessage);

                return View(model);
            }
            catch
            {
                return View("PageNotFound");
            }
        }

        [HttpPost("profile")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Index(ManageViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
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

                var phoneNumber = user.PhoneNumber;
                if (model.PhoneNumber != phoneNumber)
                {
                    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                    if (!setPhoneResult.Succeeded)
                    {
                        throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                    }
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Address = model.Address;
                user.City = model.City;
                user.Country = model.Country;
                user.PostalCode = model.PostalCode;

                await _userManager.UpdateAsync(user);

                StatusMessage = "Your profile has been updated";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("PageNotFound");
            }
        }

        [HttpGet("change-password")]
        [ResponseCache(CacheProfileName = "Short")]
        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };
                return View(model);
            }
            catch
            {
                return View("PageNotFound");
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    AddErrors(changePasswordResult);
                    return View(model);
                }

                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation("User changed their password successfully.");
                StatusMessage = "Your password has been changed.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("PageNotFound");
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost("Users/[controller]/[action]")]
        [Authorize]
        public async Task<IActionResult> Avatar(IFormFile avatarImage)
        {
            try
            {
                if (avatarImage == null)
                {
                    this.StatusMessage = "Error: Please provide an image!";
                    return this.RedirectToAction(nameof(Index));
                }

                if (!this.IsValidImage(avatarImage))
                {
                    this.StatusMessage = "Error: Image is too large or incorrect forma!";
                    return this.RedirectToAction(nameof(Index));
                }

                await this._userService.SaveAvatarImageAsync(
                    avatarImage.OpenReadStream(),
                    this.User.GetId());

                this.StatusMessage = "Profile image successfully updated.";

                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("PageNotFound");
            }
        }

        [NonAction]
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        [NonAction]
        private bool IsValidImage(IFormFile image)
        {
            string type = image.ContentType;
            /*Checks the format of the image/*/
            if (type != "image/png" && type != "image/jpg" && type != "image/jpeg")
            {
                return false;
            }

            /*Checks if the file is smaller than 1 MB.*/
            return image.Length < 1024 * 1024;
        }

        [NonAction]
        private string GetUploadRoot()
        {
            var environment = this.HttpContext.RequestServices
                .GetService(typeof(IHostingEnvironment)) as IHostingEnvironment;

            return Path.Combine(environment.WebRootPath, "images", "avatars");
        }
    }
}
