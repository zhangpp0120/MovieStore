using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Core.Entities;

namespace MovieStore.Infrastructure.Data
{
    // Install all the EF Core libraries using Nuget package Manager
    // Create a class that inherites from DbContext class
    // DbContext kinda represents your database.

    // Create a connection string which EF is gonna use to create/access the Database, which should include Server name,
    // Database Name and any credentials.
    // Create an Entity Class, Genre table.
    // Make sure to add your Entity Class as a Dbset property inside your Dbcontext class.
    // In EF we have thing called Migrations, we are gonna use Migrations to create our Database
    // We need to add Migration commands to create the tables, database etc.
    // 1. Add-Migration MigrationName, make sure your migration names are meaningful, dont use name like, xyz, abc.
    // 2. When running migration commands, make sure the project selected is the project wihch has DbContext class (infrastructure).
    // 3. make sure you add reference for library that has DbContext to your startup.cs file, in this case MVC.
    // 4. Tell MVC project that we are using Entity Framework in startupfiles.
    // Add DbContext options as constructor parameter for our DbContext.
    // Add-Migration MigrationName, make sure your migration names are meaningful, don't use names such as xyz, abc, migration1 like that
    // Make sure you have Migration folder created, and check the creted Migration file.  "Add-Migration InitialMigration"
    // After Creating Migration file and verifying it we need to use update-database command    "update-database"

    // Whenever you change your model/entity always make sure you add new Migration
    // with Code first approach, never change the database directly, always change the entities using DataAnnotations or Fluent API and add new migration then update DB


    // IN EF we have 2 ways to create our entities and model our database using CODE-FIRST approach.
    // 1. Data Annotations which is nothing but bunch of C# attributes that we can use.
    // 2. Fluent API is more syntax and more powerful and usually uses lambdas
    // Combine both DataAnnotations and Fluent API




    public class MovieStoreDbContext:DbContext
    {
        // Multiple Dbsets, all the Dbsets you create are gonna reside in your DbContext class.
        // This DbSet, is gonna represent out Table based on Entity class which is Genre in this case

        public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : base(options)
        {

        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }

        //fluent API 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
        }

        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> modelBuilder)
        {
            modelBuilder.ToTable("MovieGenre");
            modelBuilder.HasKey(mg => new { mg.MovieId, mg.GenreId });
            modelBuilder.HasOne(mg => mg.Movie).WithMany(g => g.MovieGenres).HasForeignKey(mg => mg.MovieId);
            modelBuilder.HasOne(mg => mg.Genre).WithMany(g => g.MovieGenres).HasForeignKey(mg => mg.GenreId);
        }

        private void ConfigureTrailer(EntityTypeBuilder<Trailer> modelBuilder)
        {
            modelBuilder.ToTable("Trailer");
            modelBuilder.HasKey(t => t.Id);
            modelBuilder.Property(t => t.Name).HasMaxLength(2084);
            modelBuilder.Property(t => t.TrailerUrl).HasMaxLength(2084);
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> modelBuilder)
        {
            // we can use Fluent API configuration to model our tables.
            //modelBuilder.ToTable("Movie");
            //modelBuilder.HasKey(m => m.Id);
            //modelBuilder.Property(m => m.Title).IsRequired().HasMaxLength(256);

            modelBuilder.ToTable("Movie");
            modelBuilder.HasKey(m => m.Id);
            modelBuilder.Property(m => m.Title).IsRequired().HasMaxLength(256);
            modelBuilder.Property(m => m.Overview).HasMaxLength(4096);
            modelBuilder.Property(m => m.Tagline).HasMaxLength(512);
            modelBuilder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            modelBuilder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            modelBuilder.Property(m => m.PosterUrl).HasMaxLength(2084);
            modelBuilder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            modelBuilder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            modelBuilder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            modelBuilder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");

            // we dont want to create Rating column but we want C# rating property in our Entity so that we can show movie rating in the UI
            modelBuilder.Ignore(m => m.Rating);
        }
    }
}
