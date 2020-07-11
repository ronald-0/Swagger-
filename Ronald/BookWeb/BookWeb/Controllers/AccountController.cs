using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookWeb.Dtos;
using BookWeb.Entities;
using BookWeb.Enums;
using BookWeb.Interface;
using BookWeb.Models;

namespace BookWeb.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccount _account;

        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(IAccount account, SignInManager<ApplicationUser> signInManager)
        {
            _account = account;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                Alert("Login Unsuccesful!", NotificationType.error);
                ModelState.AddModelError("", "UserName/Password is incorrect");
                return View();
            }
            var signin = await _account.Login(login);
            if (signin)
            {
                Alert("Login successful.", NotificationType.success);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        //////////

        public async Task<IActionResult> SignUp(UserDtos u)
        {
            if (!ModelState.IsValid)
            {
                Alert("Sign Up Unsuccesful!", NotificationType.error);
                ModelState.AddModelError("", "UserName/Password is incorrect");
                return View();
            }
            ApplicationUser user = new ApplicationUser();
            user.UserName = u.Username;
            user.Email = u.Email;

            var signUp = await _account.Signup(user, u.Password);

            if (signUp)
            {
                Alert("Account Created successfully.", NotificationType.success);
                return RedirectToAction("login", "Account");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {

            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}