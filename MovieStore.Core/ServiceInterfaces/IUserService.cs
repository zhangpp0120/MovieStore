using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel);
        Task<UserLoginReponseModel> ValidateUser(string email, string password);
        Task<Purchase> Purchase(PurchaseRequestModel purchaseRequestModel);
        Task<IEnumerable< Review>> GetUserReview(int userId);
        Task<Favorite> AddFavorite(Favorite favorite);
        Task DeleteFavorite(Favorite favorite);
        Task<bool> IsFavorite(Favorite favorite);
    }



}
