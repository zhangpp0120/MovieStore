using MovieStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStore.Core.Models.Request
{
    public class MovieRequestModel
    {
        public Movie Movie { get; set; }
        public bool Purchased { get; set; }
        public bool Favorited { get; set; }
    }
}
