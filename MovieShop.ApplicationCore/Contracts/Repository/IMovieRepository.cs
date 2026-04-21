using MovieShop.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieShop.ApplicationCore.Contracts.Repository
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetHighestGrossingMovies();
        Task<Movie?> GetMovieById(int id);
    }
}