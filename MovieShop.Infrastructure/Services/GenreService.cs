using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Entities;
using MovieShop.ApplicationCore.Helper;
using MovieShop.ApplicationCore.Models.Request;
using MovieShop.ApplicationCore.Models.Response;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<Page<GenreResponse>> GetPage(int pageNumber = 1, int pageSize = 3)
        {
            
            var genrePage = await _genreRepository.GetGenreByPagination(pageNumber, pageSize);

            var responsePage = new Page<GenreResponse>
            {
                PageNumber = genrePage.PageNumber,
                TotalRecords = genrePage.TotalRecords,
                Data = genrePage.Data.Select(g => new GenreResponse
                {
                    Id = g.Id,
                    Name = g.Name
                })
            };

            return responsePage;
        }

        public async Task AddGenre(GenreRequest genreRequest)
        { 
            var genre = new Genre
            {
                Name = genreRequest.Name
            };

            await _genreRepository.Add(genre);
        }
    }
}