using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;

        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public async Task<CastDetailsModel> GetCastDetails(int id)
        {
            var cast = await _castRepository.GetById(id);

            if (cast == null)
            {
                return null;
            }

            return new CastDetailsModel
            {
                Id = cast.Id,
                Name = cast.Name,
                ProfilePath = cast.ProfilePath,
                Gender = cast.Gender,
                Movies = cast.MovieCasts.Select(mc => new MovieCardModel
                {
                    Id = mc.Movie.Id,
                    Title = mc.Movie.Title,
                    PosterUrl = mc.Movie.PosterUrl
                }).ToList()
            };
        }
    }
}