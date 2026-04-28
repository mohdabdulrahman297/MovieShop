using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShop.ApplicationCore.Contracts.Services;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IPurchaseService _purchaseService;

        public UserController(IFavoriteService favoriteService,
                      IPurchaseService purchaseService)
        {
            _favoriteService = favoriteService;
            _purchaseService = purchaseService;
        }

        // GET: /User/Favorites
        public async Task<IActionResult> Favorites()
        {
            var userId = GetUserId();
            var favorites = await _favoriteService.GetFavoritesByUser(userId);
            return View(favorites);
        }

        // POST: /User/AddFavorite
        [HttpPost]
        public async Task<IActionResult> AddFavorite(int movieId)
        {
            var userId = GetUserId();
            await _favoriteService.AddFavorite(userId, movieId);
            return RedirectToAction("Details", "Movies", new { id = movieId });
        }

        // POST: /User/RemoveFavorite
        [HttpPost]
        public async Task<IActionResult> RemoveFavorite(int movieId)
        {
            var userId = GetUserId();
            await _favoriteService.RemoveFavorite(userId, movieId);
            return RedirectToAction("Details", "Movies", new { id = movieId });
        }

        // ── Helper ───────────────────────────────────────────
        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        // GET: /User/Purchases
        public async Task<IActionResult> Purchases()
        {
            var userId = GetUserId();
            var purchases = await _purchaseService.GetPurchasesByUser(userId);
            return View(purchases);
        }

        // POST: /User/BuyMovie
        [HttpPost]
        public async Task<IActionResult> BuyMovie(int movieId)
        {
            try
            {
                var userId = GetUserId();
                await _purchaseService.BuyMovie(userId, movieId);
                return RedirectToAction("Purchases");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Details", "Movies", new { id = movieId });
            }
        }
    }
}