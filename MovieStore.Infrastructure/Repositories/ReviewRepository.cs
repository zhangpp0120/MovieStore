using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Infrastructure.Data;
using MovieStore.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Infrastructure.Repositories
{
    public class ReviewRepository:EfRepository<Review>, IReviewRepository
    {
        public ReviewRepository(MovieStoreDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable< Review>> GetUserReview(int userId)
        {
            var reviews = await _dbContext.Reviews.Include(r => r.Movie).Where(r => r.UserId == userId).ToListAsync();
                //(from r in _dbContext.Reviews join p in _dbContext.Purchases on m.Id equals p.MovieId where p.UserId == userId select m).ToListAsync();
            return reviews;
        }
    }
}
