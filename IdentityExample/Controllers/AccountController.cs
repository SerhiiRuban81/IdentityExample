using IdentityExample.Models;
using IdentityExample.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityExample.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Login, YearOfBirth = model.YearOfBearth };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            LoginViewModel model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = await userManager.FindByNameAsync(model.Login);
                var result = await signInManager.PasswordSignInAsync(model.Login, model.Password, model.IsPersistent, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Користувача не знайдено і/або пароль не вірний");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }


        [AllowAnonymous]
        public IActionResult GoogleAuth()
        {
            string redirectUrl = Url.Action("GoogleRedirect", "Account");
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }


        [AllowAnonymous]
        public IActionResult FbAuth()
        {
            string redirectUrl = Url.Action("GoogleRedirect", "Account");
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Facebook", redirectUrl);
            return new ChallengeResult("Facebook", properties);
        }
        [AllowAnonymous]
        public async Task<IActionResult> GoogleRedirect()
        {
            ExternalLoginInfo loginInfo = await signInManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
                return RedirectToAction("Login");
            var loginResult = await signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, false);

            string[] userInfo = { loginInfo.Principal.FindFirst(ClaimTypes.Name).Value,
                    loginInfo.Principal.FindFirst(ClaimTypes.Email).Value};
            if (loginResult.Succeeded)
            {
                return View(userInfo);
            }
            User user = new User
            {
                UserName = loginInfo.Principal.FindFirst(ClaimTypes.Email).Value,
                Email = loginInfo.Principal.FindFirst(ClaimTypes.Email).Value
            };
            var result = await userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                result = await userManager.AddLoginAsync(user, loginInfo);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return View(userInfo);
                }
            }
            return RedirectToAction("AccessDenied");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
