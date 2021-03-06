using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;
using MovieStore.Infrastructure.Data;
using MovieStore.Infrastructure.Repositories;
using MovieStore.Infrastructure.Services;
using MovieStore.MVC.Helpers;

namespace MovieStore.MVC
{
    // this is the one of the most important class in your application.

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<MovieStoreDbContext>(options=>
                options.UseSqlServer(Configuration.GetConnectionString("MovieStoreDbConnection")));

            services.AddMemoryCache();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(
                options => { options.Cookie.Name = "MovieStoreAuthCookie";
                    options.ExpireTimeSpan = TimeSpan.FromHours(2);
                    options.LoginPath = "/Account/Login";
                }
                );

            // DI in ASP.net core has 3 types of lifetime, scoped, singleton, transient.
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<ICryptoService, CryptoService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // self defined middleware.
                //app.UseMovieStoreExceptionMiddleware();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            // middleware order matters.
            // you can create your own custom middleware.
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //Routing -- Pattern matching technique
                // http:localhost:2222/Movies/index/3
                // 1. traditional way of routing
                // 2. Attribute 

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                // id? default for this is null, which is   ../Home/Index
                // instead of ../Home/Index/0
            });
        }
    }
}
