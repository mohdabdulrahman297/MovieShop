using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Entities;
using MovieShop.ApplicationCore.Models;

namespace MovieShop.Infrastructure.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMovieRepository _movieRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository,
                               IMovieRepository movieRepository)
        {
            _purchaseRepository = purchaseRepository;
            _movieRepository = movieRepository;
        }

        public async Task<PurchaseModel> BuyMovie(int userId, int movieId)
        {
            // Prevent duplicate purchase
            var already = await _purchaseRepository.IsPurchased(userId, movieId);
            if (already)
                throw new Exception("You have already purchased this movie.");

            var movie = await _movieRepository.GetById(movieId)
                        ?? throw new Exception("Movie not found.");

            var purchase = new Purchase
            {
                UserId = userId,
                MovieId = movieId,
                PurchaseNumber = Guid.NewGuid(),
                TotalPrice = movie.Price,
                PurchaseDateTime = DateTime.UtcNow
            };

            var created = await _purchaseRepository.AddPurchase(purchase);

            return new PurchaseModel
            {
                Id = created.Id,
                UserId = created.UserId,
                MovieId = created.MovieId,
                Title = movie.Title,
                PosterUrl = movie.PosterUrl,
                TotalPrice = created.TotalPrice,
                PurchaseNumber = created.PurchaseNumber,
                PurchaseDateTime = created.PurchaseDateTime
            };
        }

        public async Task<bool> IsPurchased(int userId, int movieId)
        {
            return await _purchaseRepository.IsPurchased(userId, movieId);
        }

        public async Task<IEnumerable<PurchaseModel>> GetPurchasesByUser(int userId)
        {
            var purchases = await _purchaseRepository.GetPurchasesByUser(userId);
            return purchases.Select(p => new PurchaseModel
            {
                Id = p.Id,
                UserId = p.UserId,
                MovieId = p.MovieId,
                Title = p.Movie.Title,
                PosterUrl = p.Movie.PosterUrl,
                TotalPrice = p.TotalPrice,
                PurchaseNumber = p.PurchaseNumber,
                PurchaseDateTime = p.PurchaseDateTime
            });
        }
    }
}