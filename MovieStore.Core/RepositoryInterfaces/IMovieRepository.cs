using MovieStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.RepositoryInterfaces
{
    public interface IMovieRepository: IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetHighestRevenueMovie();

        Task<IEnumerable<Movie>> GetTop25RatedMovies();

        Task<IEnumerable<Movie>> GetMoviesByGenreId(int genreId);
        Task<decimal> GetMovieRatingById(int movieId);
        Task<IEnumerable<MovieCast>> GetMovieCastsById(int movieId);
        Task<IEnumerable<Movie>> GetMovieByUser(int userId);
        Task<IEnumerable<Movie>> GetFavoriteMovieByUser(int userId);

        Task<bool> IsMovieFavorited(Favorite favorite);
        Task<bool> IsMoviePurchased(Purchase purchase);
    }

    // IAsyncRepository has 8 methods
}
