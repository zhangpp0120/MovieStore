using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Infrastructure.Repositories
{
    public class FavoriteRepository:EfRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(MovieStoreDbContext dbContext):base(dbContext)
        {

        }

        public async Task<bool> GetFavoriteByUser(Favorite favorite)
        {
            var favorites = await _dbContext.Favorites.Where(f => f.UserId == favorite.UserId).ToListAsync();
            return favorites.Contains(favorite);
        }

        public async Task<bool> CheckFavorite(Favorite favorite)
        {
            var favorites = await _dbContext.Favorites.Where(f => f.UserId == favorite.UserId).ToListAsync();
            return favorites.Contains(favorite);
        }

        public async Task DeleteFavorite(Favorite favorite)
        {
            var delFavorite = await _dbContext.Favorites.Where(f => f.MovieId== favorite.MovieId && f.UserId== favorite.UserId).ToListAsync();
            foreach (var f in delFavorite)
            {
                await this.DeleteAsync(f);
            }
            
        }
    }
}
