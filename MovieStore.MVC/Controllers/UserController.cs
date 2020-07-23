using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.MVC.Controllers
{
    public class UserController : Controller
    {
        // 1.  Purchase a Movie, HttpPost, store that info in the Purchase table
        // http:localhost:12112/User/Purchase -- HttpPost
        // first check whether the user already bought that movie
        // BUY Buuton in the Movie Details Page will call the above method
        // if user already bought that movie, then replace Buy button with Watch Movie button

        // 2. Get all the Movies Purchased by user, loged in User, take userid from HttpContext and get all the movies
        // and give them to Movie Card partial view
        // http:localhost:12112/User/Purchases -- HttpGet
        // 3. Create a Review for a Movie for Loged In user , take userid from HttpContext and save info in Review Table
        // http:localhost:12112/User/review -- HttpPost
        // Review Button will open a popup and ask user to enter a small review in textarea and have him enter
        // movie rating between 1 and 10 and then save
        // 4. Get all the Reviews done my loged in User,
        // http:localhost:12112/User/reviews -- HttpGet

        // 5. Add a Favorite Movie for Loged In User
        // http:localhost:12112/User/Favorite -- HttpPost
        // add another button called favorite, same conecpt as Purchase
        // FontAweomse libbary and use buttons from there
        // 6.Check if a particular Movie has been added as Favorite by logedin user
        // http:localhost:12112/User/{123}/movie/{12}/favorite  HttpGet   return boolean
        // 7. Remove favorite
        // http:localhost:12112/User/Favorite -- Httpdelete


        // Filters in ASP.NET[Attributes]
        // Some piece of code that runs either before an controller or action method executes or when some event happens
        // that run before or after specific stages in the Http Pipeline
        // 1. Authorization ---
        // 2. Action Filter
        // 3. Result Filter
        // 4. Exception filter, but in real world we used Exception middleware to catch exceptions
        // 5. Resource filter
        //  who can call this purchase method???
        // Only Authorized user, user should have entered his un/pw and valid then only we need to execute this method



        private readonly IUserService _userService;
        private readonly IMovieService _movieService;
        private readonly IReviewService _reviewService;
        public UserController(IUserService userService, IMovieService movieService, IReviewService reviewService)
        {
            _userService = userService;
            _movieService = movieService;
            _reviewService = reviewService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Favorite(Favorite favorite)
        {
            favorite.UserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var createdReview = await _userService.AddFavorite(favorite);
            return LocalRedirect("/");
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Review (Review review)
        {
            review.UserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var createdReview = await _reviewService.AddReview(review);
            return LocalRedirect("/");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Reviews()
        {
            var userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var reviews = await _userService.GetUserReview(userId);

            return View(reviews);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult>Purchase(PurchaseRequestModel purchaseRequestModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                purchaseRequestModel.UserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

                var moviePurchased = await _userService.Purchase(purchaseRequestModel);
                return LocalRedirect("/");
            }
            return LocalRedirect("/Account/Login");
            
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> MoviePurchased()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                var movies = await _movieService.GetMovieByUser(userId);

                return View("/Views/Movies/Genre.cshtml", movies);
            }

            return LocalRedirect("/Account/Login");

        }
    }
}
