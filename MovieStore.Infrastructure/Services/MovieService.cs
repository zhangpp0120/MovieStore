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
            throw new NotImplementedException();
        }

        public async Task<Movie> GetMovieById(int id)  
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetMovieCount(string title = "")
        {
            return await _movieRepository.GetMovieCounts(title);
        }

        public async Task<IEnumerable<Movie>> GetTop25HighestRevenueMovies()
        {
            return await _movieRepository.GetHighestRevenueMovie();
        }

        public async Task<IEnumerable<Movie>> GetTop25RatedMovies()  // this is homwork to use LinQ
        {
            return await _movieRepository.GetTop25RatedMovies();
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
            throw new NotImplementedException();
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
