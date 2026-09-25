using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Entities;
using System.Security.Claims;

namespace MovieShopMVC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET /api/reviews
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _reviewService.GetAllAsync();
            return Ok(reviews.Select(ToResponse));
        }

        // GET /api/reviews/movie/5
        [HttpGet("movie/{movieId:int}")]
        public async Task<IActionResult> GetByMovie(int movieId)
        {
            if (movieId <= 0)
                return BadRequest(new { message = "Movie ID must be positive." });

            var reviews = await _reviewService.GetByMovieIdAsync(movieId);
            return Ok(reviews.Select(ToResponse));
        }

        // GET /api/reviews/5/10
        // In this example: userId = 5, movieId = 10.
        [HttpGet("{userId:int}/{movieId:int}")]
        public async Task<IActionResult> GetById(int userId, int movieId)
        {
            if (userId <= 0 || movieId <= 0)
                return BadRequest(new { message = "IDs must be positive." });

            var review = await _reviewService.GetByIdAsync(userId, movieId);

            return review is null
                ? NotFound(new { message = "Review not found." })
                : Ok(ToResponse(review));
        }

        // POST /api/reviews
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewRequest request)
        {
            if (!TryGetCurrentUserId(out int userId))
                return Unauthorized(new { message = "User ID claim is missing or invalid." });

            var review = new Review
            {
                UserId = userId,
                MovieId = request.MovieId,
                Rating = request.Rating,
                ReviewText = request.ReviewText ?? string.Empty
            };

            try
            {
                var created = await _reviewService.AddAsync(review);

                return CreatedAtAction(
                    nameof(GetById),
                    new { userId = created.UserId, movieId = created.MovieId },
                    ToResponse(created));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // PUT /api/reviews/10
        // The URL identifies the movie; the token identifies the review's owner.
        [Authorize]
        [HttpPut("{movieId:int}")]
        public async Task<IActionResult> Update(
            int movieId, [FromBody] UpdateReviewRequest request)
        {
            if (!TryGetCurrentUserId(out int userId))
                return Unauthorized(new { message = "User ID claim is missing or invalid." });

            var review = new Review
            {
                UserId = userId,
                MovieId = movieId,
                Rating = request.Rating,
                ReviewText = request.ReviewText ?? string.Empty
            };

            try
            {
                bool updated = await _reviewService.UpdateAsync(review);
                return updated
                    ? NoContent()
                    : NotFound(new { message = "Your review for this movie was not found." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE /api/reviews/10
        [Authorize]
        [HttpDelete("{movieId:int}")]
        public async Task<IActionResult> Delete(int movieId)
        {
            if (!TryGetCurrentUserId(out int userId))
                return Unauthorized(new { message = "User ID claim is missing or invalid." });

            if (movieId <= 0)
                return BadRequest(new { message = "Movie ID must be positive." });

            bool deleted = await _reviewService.DeleteAsync(userId, movieId);

            return deleted
                ? NoContent()
                : NotFound(new { message = "Your review for this movie was not found." });
        }

        private bool TryGetCurrentUserId(out int userId)
        {
            string? value = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            return int.TryParse(value, out userId) && userId > 0;
        }

        private static ReviewResponse ToResponse(Review review) =>
            new(
                review.UserId,
                review.MovieId,
                review.Rating,
                review.ReviewText,
                review.CreatedDate);
    }

    public record CreateReviewRequest(
        int MovieId,
        decimal Rating,
        string? ReviewText);

    public record UpdateReviewRequest(
        decimal Rating,
        string? ReviewText);

    public record ReviewResponse(
        int UserId,
        int MovieId,
        decimal Rating,
        string ReviewText,
        DateTime CreatedDate);
}