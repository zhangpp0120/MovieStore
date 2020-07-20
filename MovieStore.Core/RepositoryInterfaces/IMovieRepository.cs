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

        Task<IEnumerable<Movie>> GetMoviesByGenreId(int gId);
        Task<decimal> GetMovieRatingById(int mId);
        Task<IEnumerable<MovieCast>> GetMovieCastsById(int mId);
    }

    // IAsyncRepository has 8 methods
}
