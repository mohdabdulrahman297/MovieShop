using MovieShop.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieShop.ApplicationCore.Contracts.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<IEnumerable<Review>> GetByMovieIdAsync(int movieId);
        Task<Review?> GetByIdAsync(int userId, int movieId);
        Task<Review> AddAsync(Review review);
        Task<bool> UpdateAsync(Review review);
        Task<bool> DeleteAsync(int userId, int movieId);
    }
}