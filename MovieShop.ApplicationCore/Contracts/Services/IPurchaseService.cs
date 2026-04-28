using MovieShop.ApplicationCore.Models;

namespace MovieShop.ApplicationCore.Contracts.Services
{
    public interface IPurchaseService
    {
        Task<PurchaseModel> BuyMovie(int userId, int movieId);
        Task<bool> IsPurchased(int userId, int movieId);
        Task<IEnumerable<PurchaseModel>> GetPurchasesByUser(int userId);
    }
}