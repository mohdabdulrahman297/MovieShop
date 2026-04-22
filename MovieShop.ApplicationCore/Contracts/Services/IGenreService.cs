using MovieShop.ApplicationCore.Helper;
using MovieShop.ApplicationCore.Models.Request;
using MovieShop.ApplicationCore.Models.Response;
using System.Threading.Tasks;

namespace MovieShop.ApplicationCore.Contracts.Services
{
    public interface IGenreService
    {
        Task<Page<GenreResponse>> GetPage(int pageNumber = 1, int pageSize = 3);
        Task AddGenre(GenreRequest genreRequest);                      
    }
}