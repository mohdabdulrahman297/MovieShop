using MovieShop.ApplicationCore.Entities;

namespace MovieShop.ApplicationCore.Contracts.Repository
{
    public interface IPurchaseRepository
    {
        Task<Purchase?> GetPurchase(int userId, int movieId);
        Task<IEnumerable<Purchase>> GetPurchasesByUser(int userId);
        Task<Purchase> AddPurchase(Purchase purchase);
        Task<bool> IsPurchased(int userId, int movieId);
    }
}