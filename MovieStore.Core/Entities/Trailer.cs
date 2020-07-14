using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStore.Core.Entities
{
    public class Trailer
    {
        public int Id { get; set; }
        public string TrailerUrl { get; set; }
        public string Name { get; set; }

        // foreign key property
        public int MovieId { get; set; }
        // if some one gave you trailer id and you want to find that movie details, then this property will be useful.
        // Navigation property
        public Movie Movie { get; set; }
    }
}
