using MovieShop.ApplicationCore.Entities;

namespace MovieShop.ApplicationCore.Contracts.Repository
{
    public interface IFavoriteRepository
    {
        Task<Favorite?> GetFavorite(int userId, int movieId);
        Task<IEnumerable<Favorite>> GetFavoritesByUser(int userId);
        Task<Favorite> AddFavorite(Favorite favorite);
        Task RemoveFavorite(Favorite favorite);
        Task<bool> IsFavorited(int userId, int movieId);
    }
}