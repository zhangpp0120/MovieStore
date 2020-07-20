using MovieStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.ServiceInterfaces
{
    public interface IMovieService
    {
        //only methods that needed for users
        Task<IEnumerable<Movie>> GetTop25HighestRevenueMovies();
        Task<IEnumerable<Movie>> GetTop25RatedMovies(); // this is homwork to use LinQ 
        Task<Movie> GetMovieById(int id);
        Task<Movie> CreateMovie(Movie movie);
        Task<Movie> UpdateMovie(Movie movie);
        Task<int> GetMovieCount(string title = ""); // pagination ??
        Task<IEnumerable<Movie>> GetMoviesByGenreId(int gId);

    }
}
