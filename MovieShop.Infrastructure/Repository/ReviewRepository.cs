using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly string _connectionString;

        public ReviewRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MovieShopDbConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'MovieShopDbConnection' was not found.");
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            const string sql = """
                SELECT UserId, MovieId, Rating, ReviewText, CreatedDate
                FROM dbo.Reviews
                ORDER BY CreatedDate DESC
                """;

            return await connection.QueryAsync<Review>(sql);
        }

        public async Task<IEnumerable<Review>> GetByMovieIdAsync(int movieId)
        {
            using var connection = new SqlConnection(_connectionString);

            const string sql = """
                SELECT UserId, MovieId, Rating, ReviewText, CreatedDate
                FROM dbo.Reviews
                WHERE MovieId = @MovieId
                ORDER BY CreatedDate DESC
                """;

            return await connection.QueryAsync<Review>(
                sql, new { MovieId = movieId });
        }

        public async Task<Review?> GetByIdAsync(int userId, int movieId)
        {
            using var connection = new SqlConnection(_connectionString);

            const string sql = """
                SELECT UserId, MovieId, Rating, ReviewText, CreatedDate
                FROM dbo.Reviews
                WHERE UserId = @UserId AND MovieId = @MovieId
                """;

            return await connection.QuerySingleOrDefaultAsync<Review>(
                sql, new { UserId = userId, MovieId = movieId });
        }

        public async Task<Review> AddAsync(Review review)
        {
            using var connection = new SqlConnection(_connectionString);

            const string sql = """
                INSERT INTO dbo.Reviews
                    (UserId, MovieId, Rating, ReviewText, CreatedDate)
                OUTPUT
                    INSERTED.UserId,
                    INSERTED.MovieId,
                    INSERTED.Rating,
                    INSERTED.ReviewText,
                    INSERTED.CreatedDate
                VALUES
                    (@UserId, @MovieId, @Rating, @ReviewText, @CreatedDate)
                """;

            return await connection.QuerySingleAsync<Review>(
                sql,
                new
                {
                    review.UserId,
                    review.MovieId,
                    review.Rating,
                    review.ReviewText,
                    review.CreatedDate
                });
        }

        public async Task<bool> UpdateAsync(Review review)
        {
            using var connection = new SqlConnection(_connectionString);

            const string sql = """
                UPDATE dbo.Reviews
                SET Rating = @Rating,
                    ReviewText = @ReviewText
                WHERE UserId = @UserId AND MovieId = @MovieId
                """;

            int rowsAffected = await connection.ExecuteAsync(
                sql,
                new
                {
                    review.UserId,
                    review.MovieId,
                    review.Rating,
                    review.ReviewText
                });

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int userId, int movieId)
        {
            using var connection = new SqlConnection(_connectionString);

            const string sql = """
                DELETE FROM dbo.Reviews
                WHERE UserId = @UserId AND MovieId = @MovieId
                """;

            int rowsAffected = await connection.ExecuteAsync(
                sql, new { UserId = userId, MovieId = movieId });

            return rowsAffected > 0;
        }
    }
}