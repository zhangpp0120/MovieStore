using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieStore.MVC.Models;

namespace MovieStore.MVC.Controllers
{
    // any c# class can become a MVC controller if it inherits from controller class from Microsoft.AspNetCore.Mvc


    // http://localhost:2333/Home/index
    /* Routing -- Pattern matching technique
     * HomeController
     * Index -- Action method
     */

    public class HomeController : Controller
    {
       public IActionResult Index()
        {
            // here we need to return a instance of a class that implements that IActionResult.
            // By default MVC will look for the same name as Action method name
            // it will look inside Views foler --> Home(same name as controller) --> index.cshtml

            // 1. Program.cs ==> Main method
            // 2. StartUp Class
            // 3. ConfigureServices
            // 4. Configure
            // 5. HomeController
            // 6. Action method
            // 7. Return a view

            //In ASP.NET Core Middleware ... a piece of software logic that will be executed...
            // request --> M1 --> some process --> next M2 -->M3 ... M5  --> Response 
            return View();
        }
    }
}
