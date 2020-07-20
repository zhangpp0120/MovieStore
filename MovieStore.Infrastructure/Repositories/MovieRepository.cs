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

        public async Task<IEnumerable<Movie>> GetHighestRevenueMovie()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(25).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenreId(int gId)
        {
            //select * from movie m join moviegenre mg on m.id = mg.movieid where mg.genreid = gId
            var movies = await(from m in _dbContext.Movies join mg in _dbContext.MovieGenres on m.Id equals mg.MovieId where mg.GenreId == gId select m).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetTop25RatedMovies()
        {
            //var movies = await (from r in _dbContext.Reviews
            //                join m in _dbContext.Movies on r.MovieId equals m.Id
            //                group m by r.MovieId into rw
            //                orderby rw.Average(r => r.Rating) descending
            //                select new Movie{Id = rw.Key, Rating = rw.Average(r=> r.Rating) }).Take(25).ToListAsync();


            //var reviews = _dbContext.Reviews.GroupBy(r => r.MovieId).OrderByDescending(r => r.Average(i => i.Rating)).Take(25);
            //var movies = _dbContext.Movies.Where(m => reviews.Contains( m.Id) )

            //var movies = await _dbContext.Movies
            //    .Join(_dbContext.Reviews, m => m.Id, r => r.MovieId, (m, r) => new { m, r })
            //    .GroupBy(mr => mr.m.Id)
            //    //.OrderByDescending(mr => mr.Average(i => i.r.Rating))
            //    .Take(25)
            //    .ToListAsync();


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
