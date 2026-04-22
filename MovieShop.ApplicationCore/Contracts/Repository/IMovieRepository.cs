using MovieShop.ApplicationCore.Entities;
using MovieShop.ApplicationCore.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieShop.ApplicationCore.Contracts.Repository
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetHighestGrossingMovies();
        Task<Movie?> GetMovieById(int id);
        Task<Page<Movie>> GetMoviesByGenre(int genreId, int pageNumber = 1, int pageSize = 30);
    }
}