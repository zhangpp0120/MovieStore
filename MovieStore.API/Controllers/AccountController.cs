using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
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
            return Ok(User);
        }
    }

}
