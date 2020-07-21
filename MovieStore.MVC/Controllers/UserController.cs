using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            return View();
        }
    }
}
