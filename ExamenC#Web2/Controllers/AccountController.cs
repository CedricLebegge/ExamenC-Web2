using ExamenC_Web2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamenC_Web2.Services;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using ExamenC_Web2.Models;

namespace ExamenC_Web2.Controllers
{

    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        IIdentityRepository _repo;

        public AccountController(IIdentityRepository repo, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _repo = repo;
        }

        [HttpGet]
        public IActionResult LoginWithUserName()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginWithUserName(LoginUsernameViewModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await _repo.LoginAsync(login.UserName, null, login.Password);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(login);
        }

        [HttpGet]
        public IActionResult LoginWithEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginWithEmail(LoginEmailViewModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await _repo.LoginAsync(null, login.Email, login.Password);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(login);
        }

        public IActionResult Login()
        {
            return View();
        }
        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerData)
        {
            //if(ModelState.IsValid)
            //{
            //    var identityUser = new IdentityUser { UserName = registerData.UserName, Email = registerData.Email };
            //    var result = await _userManager.CreateAsync(identityUser, registerData.Password);
            //    if(result.Succeeded)
            //    {
            //        return View("Login");
            //    }
            //    else
            //    {
            //        foreach (var error in result.Errors)
            //            ModelState.AddModelError("", error.Description);
            //        return View(registerData);
            //    }
            //}
            //return View(registerData);

            if (ModelState.IsValid)
            {
                var result = await _repo.RegisterAsync(registerData);
                if (result.Succeeded)
                {
                    return View("Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error);
                }
            }

            return View(registerData);
        }
        #endregion


        [HttpGet]
        public IActionResult LoggedIn()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _repo.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
