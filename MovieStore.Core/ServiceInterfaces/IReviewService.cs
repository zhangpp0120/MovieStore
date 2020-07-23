using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.ServiceInterfaces
{
    public interface IReviewService
    {
        Task<Review> AddReview(Review review);
    }
}
