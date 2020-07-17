using MovieStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.ServiceInterfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllGenres();
        //    Task<Genre> GetGenreById(int id);
        //    Task<Genre> CreateGenre(Genre genre);
        //    Task<Genre> UpdateGenre(Genre Genre);
        //    Task<int> GetGenreCount(string title = "");
    }
}
