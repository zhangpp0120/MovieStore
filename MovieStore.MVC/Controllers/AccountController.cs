using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Models.Request;
using MovieStore.Core.ServiceInterfaces;
using MovieStore.Infrastructure.Services;

namespace MovieStore.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet] // user get from server 
        public ActionResult Register()
        {
            return View();

        }

        [HttpPost] // upload info to server side
        public async Task<IActionResult> Register(UserRegisterRequestModel userRegisterRequestModel)
        {
            if (ModelState.IsValid) // server side valid check, when all the checks are passed.
            {
                // now call the service
                var createdUser = await _userService.RegisterUser(userRegisterRequestModel);
                return RedirectToAction("Login");
            }
            // we take this object from the View
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel loginRequest)
        {
            if (ModelState.IsValid)
            {
                // call service layer to validate user
                var user = await _userService.ValidateUser(loginRequest.Email, loginRequest.Password);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login");
                }
            }
            return View();
        }
    }
}