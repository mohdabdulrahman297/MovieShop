using MovieShop.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.ApplicationCore.Contracts.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieCardModel>> GetTopMovies();
    }
}
