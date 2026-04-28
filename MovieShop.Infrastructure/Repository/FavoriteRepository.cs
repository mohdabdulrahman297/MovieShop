using Microsoft.EntityFrameworkCore;
using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Entities;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Repository
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly MovieDbContext _dbContext;

        public FavoriteRepository(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Favorite?> GetFavorite(int userId, int movieId)
        {
            return await _dbContext.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.MovieId == movieId);
        }

        public async Task<IEnumerable<Favorite>> GetFavoritesByUser(int userId)
        {
            return await _dbContext.Favorites
                .Include(f => f.Movie)
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

        public async Task<Favorite> AddFavorite(Favorite favorite)
        {
            await _dbContext.Favorites.AddAsync(favorite);
            await _dbContext.SaveChangesAsync();
            return favorite;
        }

        public async Task RemoveFavorite(Favorite favorite)
        {
            _dbContext.Favorites.Remove(favorite);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsFavorited(int userId, int movieId)
        {
            return await _dbContext.Favorites
                .AnyAsync(f => f.UserId == userId && f.MovieId == movieId);
        }
    }
}