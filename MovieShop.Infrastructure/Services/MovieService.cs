using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        public async Task<IEnumerable<MovieCardModel>> GetTopMovies()
        {
            var movies = new List<MovieCardModel>
            {
                new MovieCardModel
                {
                    Id = 1,
                    Title = "Interstellar",
                    PosterUrl = "https://image.tmdb.org/t/p/w500/gEU2QniE6E77NI6lCU6MxlNBvIx.jpg"
                },
                new MovieCardModel
                {
                    Id = 2,
                    Title = "The Dark Knight",
                    PosterUrl = "https://image.tmdb.org/t/p/w500/qJ2tW6WMUDux911r6m7haRef0WH.jpg"
                },
                new MovieCardModel
                {
                    Id = 3,
                    Title = "Avengers: Endgame",
                    PosterUrl = "https://image.tmdb.org/t/p/w500/or06FN3Dka5tukK1e9sl16pB3iy.jpg"
                }
            };

            return await Task.FromResult(movies);
        }
    }
}
