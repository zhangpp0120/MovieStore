using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.Models.Response;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Infrastructure.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly ICryptoService _cryptoService;
        private readonly IMovieService _movieService;
        private readonly IReviewRepository _reviewRepository;
        private readonly IFavoriteRepository _favoriteRepository;

        public UserService(IUserRepository userRepository, IPurchaseRepository purchaseRepository, ICryptoService cryptoService, IMovieService movieService, IReviewRepository reviewRepository, IFavoriteRepository favoriteRepository)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
            _movieService = movieService;
            _purchaseRepository = purchaseRepository;
            _reviewRepository = reviewRepository;
            _favoriteRepository = favoriteRepository;
        }

        public async Task<Favorite> AddFavorite(Favorite favorite)
        {
            return await _favoriteRepository.AddAsync(favorite);
        }

        public async Task DeleteFavorite(Favorite favorite)
        {
            await _favoriteRepository.DeleteFavorite(favorite);
        }

        public async Task<IEnumerable<Review>> GetUserReview(int userId)
        {
            var reviews = await _reviewRepository.GetUserReview(userId);
            return reviews;
        }

        public async Task<bool> IsFavorite(Favorite favorite)
        {
            return await _favoriteRepository.CheckFavorite(favorite);
        }

        public async Task<Purchase> Purchase(PurchaseRequestModel purchaseRequestModel)
        {
            
            var movie = await _movieService.GetMovieById(purchaseRequestModel.MovieId);
          
            var p = new Purchase
            {
                UserId = purchaseRequestModel.UserId,
                PurchaseNumber =purchaseRequestModel.PurchaseNumber.Value,
                TotalPrice = movie.Price.Value,
                MovieId = purchaseRequestModel.MovieId,
                PurchaseDateTime = purchaseRequestModel.PurchaseDate.Value,
                            
            };
            return await _purchaseRepository.AddAsync(p);
        }


        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // Step 1 : Check whether this user already exists in the database
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser != null)
            {
                // we already have this user(email) in our table
                // return or throw an exception saying Conflict user
                throw new Exception("User already registered, Please try to Login");
            }


            // Step 2: genereate a random unique salt.

            var salt = _cryptoService.GenreateSalt();

            // Step 3: never ever create your own hasing algorithm, always use industry tested/tried hashing algorithm
            // hash password with salt

            var hashedPassword = _cryptoService.HashPassword(requestModel.Password, salt);

            // create User object so that we can save it to User Table

            var user = new User
            {
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName
            };

            //setp 4: save it to database

            var createdUser = await _userRepository.AddAsync(user);

            var response = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };

            return response;
        }

       

        public async Task<UserLoginReponseModel> ValidateUser(string email, string password)
        {
            // setp 1: get user record from the database by email;

            var user = await _userRepository.GetUserByEmail(email);

            if(user == null)
            {
                // user does not exists
                throw new Exception("Register first, user does not exist.");

            }
            // step 2, if user exist, hash the password that user entered in the page  with salt from the database
            var hashedPassword = _cryptoService.HashPassword(password, user.Salt);

            // setp3, compare the hashed password with the on from database.
            if (hashedPassword == user.HashedPassword)
            {
                // password match
                // send more user details

                var response = new UserLoginReponseModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DateOfBirth = user.DateOfBirth,
                };
                return response;
            }
            return null;
        }
    }
}
