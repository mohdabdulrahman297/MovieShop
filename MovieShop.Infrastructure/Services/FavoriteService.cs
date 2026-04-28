using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Entities;
using MovieShop.ApplicationCore.Models;

namespace MovieShop.Infrastructure.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public async Task AddFavorite(int userId, int movieId)
        {
            var exists = await _favoriteRepository.IsFavorited(userId, movieId);
            if (exists) return; // already favorited

            var favorite = new Favorite { UserId = userId, MovieId = movieId };
            await _favoriteRepository.AddFavorite(favorite);
        }

        public async Task RemoveFavorite(int userId, int movieId)
        {
            var favorite = await _favoriteRepository.GetFavorite(userId, movieId);
            if (favorite == null) return;

            await _favoriteRepository.RemoveFavorite(favorite);
        }

        public async Task<bool> IsFavorited(int userId, int movieId)
        {
            return await _favoriteRepository.IsFavorited(userId, movieId);
        }

        public async Task<IEnumerable<FavoriteModel>> GetFavoritesByUser(int userId)
        {
            var favorites = await _favoriteRepository.GetFavoritesByUser(userId);
            return favorites.Select(f => new FavoriteModel
            {
                Id = f.Id,
                UserId = f.UserId,
                MovieId = f.MovieId,
                Title = f.Movie.Title,
                PosterUrl = f.Movie.PosterUrl
            });
        }
    }
}