using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.ServiceInterfaces;
using MovieStore.Infrastructure.Migrations;

namespace MovieStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMovieService _movieService;
        private readonly IReviewService _reviewService;
        public UserController(IUserService userService, IMovieService movieService, IReviewService reviewService)
        {
            _userService = userService;
            _movieService = movieService;
            _reviewService = reviewService;
        }

        //[Authorize]
        [HttpGet]
        [Route("myfavorite")]
        public async Task<IActionResult> MyFavorite()
        {
            var userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var favoriteMovies = await _movieService.GetFavoriteMovieByUser(userId);
            return Ok(favoriteMovies);
        }
        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> UnFavorite(Favorite favorite)
        //{
        //    favorite.UserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        //    await _userService.DeleteFavorite(favorite);
        //    return LocalRedirect("/User/MyFavorite");
        //}


        //[Authorize]
        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> Favorite(Favorite favorite)
        {
            //favorite.UserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var createdFavorite = await _userService.AddFavorite(favorite);
            return Ok(createdFavorite);
        }
        //[Authorize]
        [HttpPost]
        [Route("review")]
        public async Task<IActionResult> Review(Review review)
        {
            //review.UserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var createdReview = await _reviewService.AddReview(review);
            return Ok(createdReview);
        }

        //[Authorize]
        [HttpGet]
        [Route("myreviews")]
        public async Task<IActionResult> Reviews()
        {
            var userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var reviews = await _userService.GetUserReview(userId);

            return Ok(reviews);
        }

        //[Authorize]
        [HttpPost]
        [Route("purchase")]
        public async Task<IActionResult> Purchase(PurchaseRequestModel purchaseRequestModel)
        {
            //purchaseRequestModel.UserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var moviePurchased = await _userService.Purchase(purchaseRequestModel);
            //return LocalRedirect("/user/moviepurchased");
            return Ok(moviePurchased);

        }
        //[Authorize]
        [HttpGet]
        [Route("moviepurchased")]
        public async Task<IActionResult> MoviePurchased()
        {
            var userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var movies = await _movieService.GetMovieByUser(userId);

            return Ok(movies);

        }


    }
}
