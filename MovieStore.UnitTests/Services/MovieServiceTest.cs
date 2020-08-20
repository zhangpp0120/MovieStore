using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using MovieStore.Core.Entities;
using MovieStore.Infrastructure.Services;
using System.Threading.Tasks;
using MovieStore.Core.RepositoryInterfaces;
using System.Linq.Expressions;
using System.Linq;
using Moq;


namespace MovieStore.UnitTests.Services
{
    [TestFixture] // this is added to make this class a unit test class.  this is from Nunit.
    public class MovieServiceTest
    {
        // SUT System Under Test
        private MovieService _sut;

        private Mock<IMovieRepository> _mockMovieRepository;
        private List<Movie> _movies;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _movies = new List<Movie> {
                new Movie {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
                new Movie {Id = 2, Title = "Avatar", Budget = 1200000},
                new Movie {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000},
                new Movie {Id = 4, Title = "Titanic", Budget = 1200000},
                new Movie {Id = 5, Title = "Inception", Budget = 1200000},
                new Movie {Id = 6, Title = "Avengers: Age of Ultron", Budget = 1200000},
                new Movie {Id = 7, Title = "Interstellar", Budget = 1200000},
                new Movie {Id = 8, Title = "Fight Club", Budget = 1200000},
                new Movie {Id = 9, Title = "The Lord of the Rings: The Fellowship of the Ring", Budget = 1200000},
                new Movie {Id = 10, Title = "The Dark Knight", Budget = 1200000},
                new Movie {Id = 11, Title = "The Hunger Games", Budget = 1200000},
                new Movie {Id = 12, Title = "Django Unchained", Budget = 1200000},
                new Movie {Id = 13, Title = "The Lord of the Rings: The Return of the King", Budget = 1200000},
                new Movie {Id = 14, Title = "Harry Potter and the Philosopher's Stone", Budget = 1200000},
                new Movie {Id = 15, Title = "Iron Man", Budget = 1200000},
                new Movie {Id = 16, Title = "Furious 7", Budget = 1200000} };
        }

        [SetUp]
        public void SetUp()
        {
            // mock class
            _mockMovieRepository = new Mock<IMovieRepository>();

            // mock method
            _mockMovieRepository.Setup(m => m.GetHighestRevenueMovie()).ReturnsAsync(_movies);
                                                         // this means any int
            _mockMovieRepository.Setup(m => m.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int m) => _movies.First(x => x.Id == m));

        }

        [Test]
        public async Task Test_Movie_Name_And_Budget_By_MovieId()
        {
            _sut = new MovieService(_mockMovieRepository.Object);
            var movie = await _sut.GetMovieById(10);
            Assert.AreEqual(movie.Title, "The Dark Knight");
            Assert.AreEqual(1200000, movie.Budget);
        }

        [Test]
        public async Task Test_MovieId_FromFakeData_Does_not_Exist()
        {
            _sut = new MovieService(_mockMovieRepository.Object);
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _sut.GetMovieById(20));

            //var movie = await _sut.GetMovieById(10);
        }


        // Test MovieService GetTop25HighestRevenueMovies method
        // always make sure your method names are descriptive....
        [Test]
        public async Task Test_Top_25_Highest_RevenueMovies_FromFakeData()
        {
            // Unit testing should ideally not touch any external databases or resources, they should be isolated and tested independently
            // becasue in ur application you will have hundreds of mehtods that you need to be tested....if every method calls the dataabse, to run all those unit
            // tests it will take lots of time.
            // the purpose of unit test to to run as many as unit tests as possible very fast.
            // you might have 500 unit tests methods....
            // we always do our unit tests with in memory fake data....

            // it is called integration test if your test touches database.


            /// AAA
            /// Arrange, Act and Assert
            // Arrange: Initializes objects, creates mocks with arguments that are passed to the method under test and adds expectations.   
            _sut = new MovieService(_mockMovieRepository.Object);

            // Act: Invokes the method or property under test with the arranged parameters.   
            var movies = await _sut.GetTop25HighestRevenueMovies();

            // Assert: Verifies that the action of the method under test behaves as expected.
            Assert.NotNull(movies);
            Assert.AreEqual(16, movies.Count());
            CollectionAssert.AllItemsAreInstancesOfType(movies, typeof(Movie));
            //CollectionAssert.AreEquivalent(movies, IEnumerable<Movie>);


        }





    }

    public class FakeMovieRepository : IMovieRepository
    {
        public Task<Movie> AddAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync(Expression<Func<Movie, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetExistsAsync(Expression<Func<Movie, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetFavoriteMovieByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetHighestRevenueMovie()
        {
            // return list of movies, fake movies, do not go to database... we need to create fake/mock data
            var movies = new List<Movie> {
                new Movie {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
                          new Movie {Id = 2, Title = "Avatar", Budget = 1200000},
                          new Movie {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000},
                          new Movie {Id = 4, Title = "Titanic", Budget = 1200000},
                          new Movie {Id = 5, Title = "Inception", Budget = 1200000},
                          new Movie {Id = 6, Title = "Avengers: Age of Ultron", Budget = 1200000},
                          new Movie {Id = 7, Title = "Interstellar", Budget = 1200000},
                          new Movie {Id = 8, Title = "Fight Club", Budget = 1200000},
                          new Movie {Id = 9, Title = "The Lord of the Rings: The Fellowship of the Ring", Budget = 1200000},
                          new Movie {Id = 10, Title = "The Dark Knight", Budget = 1200000},
                          new Movie {Id = 11, Title = "The Hunger Games", Budget = 1200000},
                          new Movie {Id = 12, Title = "Django Unchained", Budget = 1200000},
                          new Movie {Id = 13, Title = "The Lord of the Rings: The Return of the King", Budget = 1200000},
                          new Movie {Id = 14, Title = "Harry Potter and the Philosopher's Stone", Budget = 1200000},
                          new Movie {Id = 15, Title = "Iron Man", Budget = 1200000},
                          new Movie {Id = 16, Title = "Furious 7", Budget = 1200000} };
            return await Task.FromResult(movies);
        }

        public Task<IEnumerable<Movie>> GetMovieByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MovieCast>> GetMovieCastsById(int movieId)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetMovieRatingById(int movieId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetMoviesByGenreId(int genreId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetTop25RatedMovies()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsMovieFavorited(Favorite favorite)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsMoviePurchased(Purchase purchase)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> ListAsync(Expression<Func<Movie, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> UpdateAsync(Movie entity)
        {
            throw new NotImplementedException();
        }
    }
}
