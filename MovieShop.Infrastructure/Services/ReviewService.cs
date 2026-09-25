using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public Task<IEnumerable<Review>> GetAllAsync()
        {
            return _reviewRepository.GetAllAsync();
        }

        public Task<IEnumerable<Review>> GetByMovieIdAsync(int movieId)
        {
            ValidateId(movieId, nameof(movieId));
            return _reviewRepository.GetByMovieIdAsync(movieId);
        }

        public Task<Review?> GetByIdAsync(int userId, int movieId)
        {
            ValidateId(userId, nameof(userId));
            ValidateId(movieId, nameof(movieId));

            return _reviewRepository.GetByIdAsync(userId, movieId);
        }

        public async Task<Review> AddAsync(Review review)
        {
            ValidateReview(review);

            // The entity has a composite key: one review per user/movie pair.
            var existing = await _reviewRepository.GetByIdAsync(
                review.UserId, review.MovieId);

            if (existing is not null)
            {
                throw new InvalidOperationException(
                    "This user has already reviewed this movie.");
            }

            review.ReviewText = review.ReviewText.Trim();
            review.CreatedDate = DateTime.UtcNow;

            return await _reviewRepository.AddAsync(review);
        }

        public Task<bool> UpdateAsync(Review review)
        {
            ValidateReview(review);
            review.ReviewText = review.ReviewText.Trim();

            return _reviewRepository.UpdateAsync(review);
        }

        public Task<bool> DeleteAsync(int userId, int movieId)
        {
            ValidateId(userId, nameof(userId));
            ValidateId(movieId, nameof(movieId));

            return _reviewRepository.DeleteAsync(userId, movieId);
        }

        private static void ValidateReview(Review review)
        {
            ArgumentNullException.ThrowIfNull(review);

            ValidateId(review.UserId, nameof(review.UserId));
            ValidateId(review.MovieId, nameof(review.MovieId));

            if (review.Rating < 1m || review.Rating > 10m ||
                review.Rating != decimal.Round(review.Rating, 2))
            {
                throw new ArgumentException(
                    "Rating must be between 1 and 10 with at most two decimal places.");
            }

            if (string.IsNullOrWhiteSpace(review.ReviewText))
            {
                throw new ArgumentException("Review text cannot be empty.");
            }
        }

        private static void ValidateId(int id, string parameterName)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    parameterName, "ID must be greater than zero.");
            }
        }
    }
}