using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
//namespace MovieStore.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class MoviesController : ControllerBase
//    {
//        private readonly IMovieService _movieService;
public MoviesController(IMovieService movieService)
            {
                _movieService = movieService;
            }
            // we want to construct a URL for showing top 25 revenue movies
            // [Route("api/[controller]")]
            // http:localhost/api/movies/toprevenue -- GET
            // SEO , RESTFul URL's, -- should be human readable
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
        }
    }
}

