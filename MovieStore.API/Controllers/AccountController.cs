using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Models.Request;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        // http://localhost/api/account/register

        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestModel model)
        {
            // Model Binding
            // when posting json data, make sure your json keys are same as c# model properties
            //{
            //        "firstName":"Andy",
            //        "lastName":"Wang",
            //        "email": "andy.want@gmail.com",
            //        "password":"Andy300#
            //}
            var user = await _userService.RegisterUser(model);
            return Ok(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel loginRequest)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.ValidateUser(loginRequest.Email, loginRequest.Password);

                if (user == null)
                {
                    // if login info is incorrect,  exception handler
                    ModelState.AddModelError(string.Empty, "Invalid Login");
                }

                var claims = new List<Claim> {
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);    // add var claims to the authenticationscheme

                // finally we are going to create a  cookie that will be attached to the http response
                // httpcontext is the most important class in ASP.NET, that holds all the information regarding that Http request/response
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                // Once ASP.NET Craetes Authentication Cookies, it will check for that cookie in the HttpRequest and see if the cookie is not expired
                // and it will decrypt the information present in the cookie to check whether User is Authenticated and will also get claims from the cookies

                // manually creating cookie
                //HttpContext.Response.Cookies.Append("userLanguage", "English");

                return Ok("User Login Successfully");

            }
            return LocalRedirect("~/account/login");
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return LocalRedirect("~/");
        }

    }
}
