using Microsoft.EntityFrameworkCore;
using MovieShop.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Infrastructure.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public DbSet<Trailer> Trailers { get; set; }

        public DbSet<Cast> Casts { get; set; }

        public DbSet<MovieGenre> MovieGenres { get; set; }

        public DbSet<MovieCast> MovieCasts { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(m => m.Budget).HasPrecision(18, 2);
                entity.Property(m => m.Revenue).HasPrecision(18, 2);
                entity.Property(m => m.Price).HasPrecision(5, 2);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(m => m.Budget).HasPrecision(18, 2);
                entity.Property(m => m.Revenue).HasPrecision(18, 2);
                entity.Property(m => m.Price).HasPrecision(5, 2);
            });

            modelBuilder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });


            modelBuilder.Entity<MovieCast>()
        .HasKey(mc => new { mc.MovieId, mc.CastId });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.FirstName).HasMaxLength(128).IsRequired();
                entity.Property(u => u.LastName).HasMaxLength(128).IsRequired();
                entity.Property(u => u.Email).HasMaxLength(256).IsRequired();
                entity.Property(u => u.HashedPassword).HasMaxLength(1024).IsRequired();
                entity.Property(u => u.Salt).HasMaxLength(1024).IsRequired();
                entity.Property(u => u.PhoneNumber).HasMaxLength(32);
            });
        }


    }
}
