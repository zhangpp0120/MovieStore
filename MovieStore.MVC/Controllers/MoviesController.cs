using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;
using MovieStore.MVC.Models;

namespace MovieStore.MVC.Controllers
{
    public class MoviesController : Controller
    {
        //IOC container, ASP.net has built-in IOC/DI
        // in .net Framework we needt to rely on third-party IOC to do Dependency  Injection, Autofac, Ninject.

        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // get localhost/Movies/index
        [HttpGet] // attributes in C#   put on top of a method.
        // the default is HttpGet.

        //create a  new method for the method in the controller and test
        public async Task<IActionResult> Index()
        {
            //var movies = await _movieService.GetTop25HighestRevenueMovies();
            //var movies = await _movieService.GetMovieById(1);   // inception  works
            //var movies = await _movieService.GetMovieCount("Avatar"); //1  works
            var movies = await _movieService.GetTop25RatedMovies(); // wroks
             
            return View(movies);
            // this MovieesCount is a dynamic property, you can put whatever data you want.
            //ViewBag.MoviesCount = movies.Count;
            //ViewData["myname"] = "John Doe";
            // compile time checks vs run-time checks
            // we need to pass data from Controller action method to the View
            // Usually its preferred to send a strongly typed Model or object to the view
            // 3 ways to send data from Controller to View
            // 1. Strongly-typed models (preferred way)
            // 2. ViewBag --dynamic
            // 3. ViewData - key/value
            //return View(movies);
        }

        [HttpPost]
        // here we want to create a movie, post here.
        public IActionResult Create(string title, decimal budget)
        {
            // POST http:localhost/Movies/create

            // Model binding and they are CASE IN-SENSITIVE
            // look at in-coming request and maps the input elements name/value with the parameter names of the action method
            // then the parameter will have the value automatically
            // it will also does casting/converting.
            // we need to get the data from the view and save it in database.
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            // GET  http:localhost/Movies/create
            // we need to have this method so that we can show the empty page for the user to enter movie infomation that needs to be created.

            return View();
        }
        [HttpGet]
        // if use asp-route-id, the name of that parameter is id
        // i use asp-route-gId, (in the default file of genres)
        public async Task<IActionResult> Genre(int gId)
        {
            var movies = await _movieService.GetMoviesByGenreId(gId);
            return View(movies);
        }


        [HttpGet]
        public async Task<IActionResult> MovieDetail(int mId)
        {
            var movieDetails = await _movieService.GetMovieById(mId);
            //movieDetails.Rating = await _movieService.GetMovieRatingById(mId);
            //movieDetails.MovieCasts = (ICollection<Core.Entities.MovieCast>)await _movieService.GetMovieCastsById(mId);

            return View(movieDetails);
        }
    }
}
