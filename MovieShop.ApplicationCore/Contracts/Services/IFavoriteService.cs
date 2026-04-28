using MovieShop.ApplicationCore.Models;

namespace MovieShop.ApplicationCore.Contracts.Services
{
    public interface IFavoriteService
    {
        Task AddFavorite(int userId, int movieId);
        Task RemoveFavorite(int userId, int movieId);
        Task<bool> IsFavorited(int userId, int movieId);
        Task<IEnumerable<FavoriteModel>> GetFavoritesByUser(int userId);
    }
}