using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieStore.Core.ServiceInterfaces;
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
        private readonly IMovieService _movieService;
        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [HttpGet]
       public async  Task<IActionResult> Index()
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

            //07/17 for the UI
            // we need or? Movie Card we are going to use that one in lots of places...
            // 1. Homepage -- show top revenue movies --> movie card
            // 2. Genres/ show Movies belonging to that genre --> movie card
            // 3. top reted movies--> top movies  as a card
            // we have to create this movie card in such a way that it can be reused in lots of places.
            // Partial views will helps us in creating resusable views across the application.
            // partial views are views inside another view.
            // name of it start with '_' for shared view.


            var movies = await _movieService.GetTop25HighestRevenueMovies();
            //var movies1 = await _moiveService.GetTop25RatedMovies();
            return View(movies);
        }
    }
}
