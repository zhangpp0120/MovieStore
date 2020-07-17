using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.RepositoryInterfaces
{
    // can not have access modifier for method inside of interface, default is public.
    //public interface IAsyncRepository<T> where T:class
    //{
    //    //base interface with all our CRUD (Create, read, update and delete) operations

    //    // LINQ => Collection of extenstion methods... on Ienumerable type, 
    //    // that means any class that implments IEnumerable will have access to all the extension methods in LINQ
    //    T GetById(int id);
    //    IEnumerable<T> ListAll();
    //    IEnumerable<T> ListWhere(Expression<Func<T,bool>> where);// Expresssion will be translated in SQL

    //    int GetCount(Expression<Func<T, bool>> filter = null);
    //    bool GetExists(Expression<Func<T, bool>> filter = null);
    //    T Add(T entity);
    //    T Update(T entity);
    //    void Delete(T entity);
    //}

    // Async method

    // Thread1
    //  var movieService = new MovieService();
    // 5 seconds var movies = await movieService.GetALlMovie(); i/0
    // return a Task for you
    // Improving the scalability of the application, so that you r application can server many concurrent requests properly
    // async/await will prevent thread starvation scenario.
    // I/O bound operation not CPU
    // Database calls, Http calls, over network
    // Task<Movie>, Task<int>
    // in your C# or any Library whenever you see a method with Async in the method name, that means you can await that method
    // EF, two kind of methods, normal sync method, async methods...
    // .NET 4.5, C# 5 year 2012

    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> ListAllAsync();
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter);
        Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null);
        Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
