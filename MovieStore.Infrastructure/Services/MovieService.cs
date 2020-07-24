using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;
using MovieStore.Infrastructure.Migrations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        // Constructor Injection, inject MovieRepository class instance
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Movie> CreateMovie(Movie movie)
        {
            return await _movieRepository.AddAsync(movie);
        }

        public async Task<IEnumerable<Movie>> GetFavoriteMovieByUser(int userId)
        {
            return await _movieRepository.GetFavoriteMovieByUser(userId);
        }

        public async Task<Movie> GetMovieById(int id)  
        {
            // add cache access by id or by 10 most popular;
            // like genres
            return await _movieRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Movie>> GetMovieByUser(int userId)
        {
            return await _movieRepository.GetMovieByUser(userId);
        }

        public async Task<IEnumerable<MovieCast>> GetMovieCastsById(int mId)
        {
            return await _movieRepository.GetMovieCastsById(mId);
        }

        public async Task<int> GetMovieCount(string title = "")
        {
            return await _movieRepository.GetCountAsync(m=>m.Title == title);
        }

        public async Task<decimal> GetMovieRatingById(int mId)
        {
            return await _movieRepository.GetMovieRatingById(mId);
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenreId(int gId)
        {
            return await _movieRepository.GetMoviesByGenreId(gId);
        }

        public async Task<IEnumerable<Movie>> GetTop25HighestRevenueMovies()
        {
            return await _movieRepository.GetHighestRevenueMovie();
        }

        public async Task<IEnumerable<Movie>> GetTop25RatedMovies()  // this is homwork to use LinQ
        {
            return await _movieRepository.GetTop25RatedMovies();
        }

        public async  Task<bool> IsMovieFavorited(Favorite favorite)
        {
            return await _movieRepository.IsMovieFavorited(favorite);
        }

        public async Task<bool> IsMoviePurchased(Purchase purchase)
        {
            return await _movieRepository.IsMoviePurchased(purchase);
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
            return await _movieRepository.UpdateAsync(movie);
        }
    }

    //public class MovieServiceTest : IMovieService
    //{
    //    public async Task<IEnumerable<Movie>> GetTop25HighestRevenueMovies()
    //    {
    //        var movies = new List<Movie>()
    //                    {
    //                        new Movie {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
    //                        new Movie {Id = 2, Title = "Avatar", Budget = 1200000},
    //                        new Movie {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000},
    //                        new Movie {Id = 4, Title = "Titanic", Budget = 1200000},
    //                    };
    //        return movies;
    //    }
    //}
}
