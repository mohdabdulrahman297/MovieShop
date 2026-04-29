using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShop.ApplicationCore.Contracts.Services;
using System.Security.Claims;

namespace MovieShopMVC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController(IFavoriteService favoriteService, IPurchaseService purchaseService) : ControllerBase
    {
        private int GetUserId() =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)!);

        // GET /api/user/favorites
        [HttpGet("favorites")]
        public async Task<IActionResult> GetFavorites()
        {
            var favorites = await favoriteService.GetFavoritesByUser(GetUserId());
            return Ok(favorites);
        }

        // POST /api/user/favorites/{movieId}
        [HttpPost("favorites/{movieId:int}")]
        public async Task<IActionResult> AddFavorite(int movieId)
        {
            await favoriteService.AddFavorite(GetUserId(), movieId);
            return Ok(new { message = "Movie added to favorites." });
        }

        // DELETE /api/user/favorites/{movieId}
        [HttpDelete("favorites/{movieId:int}")]
        public async Task<IActionResult> RemoveFavorite(int movieId)
        {
            await favoriteService.RemoveFavorite(GetUserId(), movieId);
            return Ok(new { message = "Movie removed from favorites." });
        }

        // GET /api/user/favorites/{movieId}/check
        [HttpGet("favorites/{movieId:int}/check")]
        public async Task<IActionResult> IsFavorited(int movieId)
        {
            var result = await favoriteService.IsFavorited(GetUserId(), movieId);
            return Ok(new { isFavorited = result });
        }

        // GET /api/user/purchases
        [HttpGet("purchases")]
        public async Task<IActionResult> GetPurchases()
        {
            var purchases = await purchaseService.GetPurchasesByUser(GetUserId());
            return Ok(purchases);
        }

        // POST /api/user/purchases/{movieId}
        [HttpPost("purchases/{movieId:int}")]
        public async Task<IActionResult> BuyMovie(int movieId)
        {
            var purchase = await purchaseService.BuyMovie(GetUserId(), movieId);
            return Ok(purchase);
        }

        // GET /api/user/purchases/{movieId}/check
        [HttpGet("purchases/{movieId:int}/check")]
        public async Task<IActionResult> IsPurchased(int movieId)
        {
            var result = await purchaseService.IsPurchased(GetUserId(), movieId);
            return Ok(new { isPurchased = result });
        }
    }
}