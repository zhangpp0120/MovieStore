using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository

    {
        public MovieRepository(MovieStoreDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Movie> GetByIdAsync(int id)
        {
            var movie = await _dbContext.Movies.FindAsync(id);
            movie.Rating = await Task.FromResult<decimal>(_dbContext.Reviews.Where(m => m.MovieId == id).Average(r => r.Rating));
            movie.MovieCasts = await _dbContext.MovieCasts.Include(mc=>mc.Cast).Where(m => m.MovieId == id).ToListAsync();
            movie.MovieGenres = await _dbContext.MovieGenres.Include(mg => mg.Genre).Where(m => m.MovieId == id).ToListAsync();

            return movie;
            //var movie = await _dbContext.Movies
            //                            .Include(m => m.MovieCasts).ThenInclude(m => m.Cast).Include(m => m.MovieGenres)
            //                            .ThenInclude(m => m.Genre)
            //                            .FirstOrDefaultAsync(m => m.Id == id);
            //if (movie == null) return null;
            //var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id).AverageAsync(r => r.Rating);
            //if (movieRating > 0) movie.Rating = movieRating;
            //return movie;


        }

        public async Task<IEnumerable<Movie>> GetHighestRevenueMovie()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(25).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<MovieCast>> GetMovieCastsById(int mId)
        {
            //sql
            //select * from cast c join moviecast mc on mc.castid = c.id where mc.movieid = mId
            var mCasts = await _dbContext.MovieCasts.Where(m => m.MovieId == mId).ToListAsync();
            return mCasts;
        }

        public async Task<decimal> GetMovieRatingById(int mId)
        {
            //sql
            //select avg(rating) from review r join movie m on m.id = r.MovieId
            //where m.id = mId
            //group by m.id
            var rating =await Task.FromResult<decimal>(_dbContext.Reviews.Where(m=>m.MovieId ==mId).Average(r=>r.Rating));
            return rating;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenreId(int gId)
        {
            //sql
            //select * from movie m join moviegenre mg on m.id = mg.movieid where mg.genreid = gId
            var movies = await(from m in _dbContext.Movies join mg in _dbContext.MovieGenres on m.Id equals mg.MovieId where mg.GenreId == gId select m).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetTop25RatedMovies()
        {
     
            // correct way 1
            var movies = await _dbContext.Reviews.Include(m => m.Movie)
                .GroupBy(r => new { r.MovieId, r.Movie.Title})
                .OrderByDescending(g => g.Average(m => m.Rating))
                .Select(m => new Movie { Id = m.Key.MovieId, Title =m.Key.Title,  Rating = m.Average(r => r.Rating) }).Take(25).ToListAsync();

            // correct way 2
            //var movies = await _dbContext.Movies.OrderByDescending(g =>g.Reviews.Average(r => r.Rating)).Take(25).ToListAsync();

            return (IEnumerable<Movie>)movies;
        }
    }
}
