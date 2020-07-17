using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Infrastructure.Repositories
{
    public class GenreRepository : EfRepository<Genre>, IGenreRepository

    {
        public GenreRepository(GenreRepository dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            //var cts = _dbContext.Genres.Count();
            //var pagination = 10;

            //var pages = cts / 10;

            //while (pages > 0)
            //{
            //    var genres = await _dbContext.Genres.Select(x => x.Name).Take(pagination).ToListAsync();
            //}

            var genres = await _dbContext.Genres.Select(x=>x.Name).ToListAsync();
            return (IEnumerable<Genre>)genres;
        }
    }

}


