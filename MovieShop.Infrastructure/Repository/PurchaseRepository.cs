using Microsoft.EntityFrameworkCore;
using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Entities;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Repository
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly MovieDbContext _dbContext;

        public PurchaseRepository(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Purchase?> GetPurchase(int userId, int movieId)
        {
            return await _dbContext.Purchases
                .FirstOrDefaultAsync(p => p.UserId == userId && p.MovieId == movieId);
        }

        public async Task<IEnumerable<Purchase>> GetPurchasesByUser(int userId)
        {
            return await _dbContext.Purchases
                .Include(p => p.Movie)
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.PurchaseDateTime)
                .ToListAsync();
        }

        public async Task<Purchase> AddPurchase(Purchase purchase)
        {
            await _dbContext.Purchases.AddAsync(purchase);
            await _dbContext.SaveChangesAsync();
            return purchase;
        }

        public async Task<bool> IsPurchased(int userId, int movieId)
        {
            return await _dbContext.Purchases
                .AnyAsync(p => p.UserId == userId && p.MovieId == movieId);
        }
    }
}