using MovieShop.ApplicationCore.Helper;
using MovieShop.ApplicationCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieShop.ApplicationCore.Contracts.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieCardModel>> GetHighestGrossingMovies();
        Task<MovieDetailsModel?> GetMovieDetails(int id);
        Task<Page<MovieCardModel>> GetMoviesByGenre(int genreId, int pageNumber = 1, int pageSize = 30); // ✅ NEW
    }
}