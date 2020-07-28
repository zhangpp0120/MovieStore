using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.API.Controllers
{
    [Route("api/[controller]")] //[controller] will be replaced with the name of your controller.
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // we want to construct a URL for showing top 25 revenue movies
        // [Route("api/[controller]")]
        // http:localhost/ap/Movies/         toprevenue -- GET
        // your URL should be human readable,  SEO (search engine optimized), RESTFul URL's

        [HttpGet]
        [Route("toprevenue")]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTop25HighestRevenueMovies();
            // in MVC we return Views
            // return data along with HTTP Status CODE
            //  OK -200

            if (!movies.Any())
            {
                return NotFound("No Movies Found!");
            }
            return Ok(movies);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(Movie movie) // movie model applied here.
        {// replace it with MovieCreateRequestModel
            return Ok(" movie created ");
        }

        [HttpGet]
        [Route("genre/{genreId}")]
        public async Task<IActionResult> Genre(int genreId)
        {
            var movies = await _movieService.GetMoviesByGenreId(genreId);
            return Ok(movies);
        }

        [HttpGet]
        [Route("moviedetail/{movieId}")]

        public async Task<IActionResult> MovieDetail(int movieId)
        {
            var m = new MovieRequestModel
            {
                Movie = await _movieService.GetMovieById(movieId),
                Purchased = false,
                Favorited = false
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                var favorite = new Favorite
                {
                    MovieId = movieId,
                    UserId = userId
                };

                var purchase = new Purchase
                {
                    MovieId = movieId,
                    UserId = userId
                };

                m.Purchased = await _movieService.IsMoviePurchased(purchase);
                m.Favorited = await _movieService.IsMovieFavorited(favorite);
            }

            //return Ok("bringback movie details"); // able to return back string
            //var movie = await _movieService.GetMovieById(movieId);
            return Ok(m);

        }

        [HttpGet]
        [Route("Watch/{movieId}")]
        public async Task<IActionResult> Watch(int movieId)
        {
            // watch movie
            var movie = await _movieService.GetMovieById(movieId);
            return Ok(movie);
        }

    }
}

