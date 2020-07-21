using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

            // http is stateless, means each every request is independent of each other. 
            // when user login, we can create a autheticate cookie, cookie is one way of storing infomation on browser, localstorage and sessionstorage.
            // cookies, if there are any cookies present then those cookies will be automatically sent to the server. 
            // when user come back again,  we need to check if the cookie is expired or not and valid or not.
            // cookies is one way fo state management, client-side.


            if (ModelState.IsValid)
            {
                // call service layer to validate user
                var user = await _userService.ValidateUser(loginRequest.Email, loginRequest.Password);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login");
                }
                // we want to show FirstName, LastName on header (navigation)
                // Claims, in asp.net you can add anything to the claim, claim works like driver license.

                // create claims based on you application needs.
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                };

                // we need to create an identity object to hold those claims
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);    // add var claims to the authenticationscheme

                // finally we are going to create a  cookie that will be attached to the http response

                // httpcontext is the most important class in ASP.NET, that holds all the information regarding that Http request/response
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                // Once ASP.NET Craetes Authentication Cookies, it will check for that cookie in the HttpRequest and see if the cookie is not expired
                // and it will decrypt the information present in the cookie to check whether User is Authenticated and will also get claims from the cookies

                // manually creating cookie
                //HttpContext.Response.Cookies.Append("userLanguage", "English");

                return LocalRedirect("~/");

            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return LocalRedirect("~/");
        }
    }
}