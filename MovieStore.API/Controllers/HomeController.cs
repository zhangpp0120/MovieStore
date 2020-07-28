using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.ServiceInterfaces;
using MovieStore.MVC.Models;

namespace MovieStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [HttpGet]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetTop25HighestRevenueMovies();
            //var movies1 = await _moiveService.GetTop25RatedMovies();
            return Ok("this is home/index page");
        }
        [HttpGet]
        [Route("error")]
        public IActionResult Error()
        {
            return Ok(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
