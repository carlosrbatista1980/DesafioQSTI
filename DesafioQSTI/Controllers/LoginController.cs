using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using DesafioQSTI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Newtonsoft.Json;
using DesafioQSTI.Data.Repositories;
using DesafioQSTI.Data.Repositories.Interfaces;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using DesafioQSTI.Data;
using DesafioQSTI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace DesafioQSTI.Controllers
{
    public class LoginController : Controller
    {
        private LoginViewModel _loginViewModel;

        private UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;

        public LoginController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        
        public IActionResult Login()
        {
            //ViewData.Model = model;
            return View();
        }

        public IActionResult TermsAndConditions(LoginViewModel loginViewModel)
        {
            return View();
        }

        public IActionResult ForgotPassword(LoginViewModel loginViewModel)
        {
            return View();
        }
        
        public IActionResult Register(LoginViewModel loginViewModel)
        {
            return View(loginViewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SignUp(LoginViewModel loginViewModel)
        {
            new LoginService().SignUpService(loginViewModel);
            if (loginViewModel._EventSuccess)
            {
                ViewData["Success"] = loginViewModel._EventMesssage;
                loginViewModel = null;
                return View("Register", loginViewModel);
            }

            if (!string.IsNullOrEmpty(loginViewModel._EventMesssage))
                ViewData["Error"] = loginViewModel._EventMesssage;
            else
                ViewData["Error"] = "Account was not created, internal error";

            return RedirectToAction("Register", loginViewModel);
            
        }
        
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult SignIn(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    Email = loginViewModel.email,
                    PasswordHash = "b49d6c03fe471ee720b5a4d56c5a9bf2",
                    UserName = loginViewModel.account,
                    EmailConfirmed = false,
                    NormalizedUserName = loginViewModel.account,
                    PhoneNumberConfirmed = false,
                    SecurityStamp = GetHashCode().ToString(),
                };
                
                //var result = await _signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, true, false);

                var userExists = new LoginService().SignInService(loginViewModel);
                if (userExists)
                {
                    //var s = await _signInManager.PasswordSignInAsync(user, user.PasswordHash, true, false);
                    //await _signInManager.SignInAsync(user, true);
                    //wait _signInManager.SignInAsync(user, true);
                    
                    
                    //User.Claims.Where(d => d.Value == ControllerContext.HttpContext.User.Claims.GetEnumerator().Current.Value);
                    loginViewModel.SessionId = GetHashCode();
                    ViewData.Model = loginViewModel;

                    

                    return RedirectToAction("Index", "Home", ViewData.Model);
                }
            }

            loginViewModel.account = "111111155555555";
            loginViewModel.password = "555555555555";
            loginViewModel.IP_user = "192.168.0.1888000";

            loginViewModel = null;

            return View("Login", loginViewModel);
            return RedirectToAction("Login", loginViewModel);
        }


        //public ActionResult SignIn(LoginViewModel loginViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new IdentityUser()
        //        {
        //            Email = loginViewModel.email,
        //            PasswordHash = loginViewModel.password,
        //            UserName = loginViewModel.account,
        //            EmailConfirmed = false,
        //            NormalizedUserName = loginViewModel.account,
        //            PhoneNumberConfirmed = false,
        //        };

        //        var result = new LoginService(_service).SignInService(loginViewModel);
        //        if (result)
        //        {
        //            var s = _signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, true, false);
        //            _signInManager.SignInAsync(user, true);
        //            ViewData["User"] = loginViewModel.account;
        //        }
        //    }

        //    return RedirectToAction("Index", "Home", loginViewModel);
        //}

        [AllowAnonymous]
        [HttpPost("autenticate")]
        public IActionResult LoginQSTI(ClienteViewModel clienteViewModel)
        {
            //var user = _userService.Authenticate(userParam.Username, userParam.Password);

            //if (user == null)
            //    return BadRequest(new { message = "Username or password is incorrect" });

            //return Ok(user);



            return View("LoginSuccess", clienteViewModel);
        }
    }
}