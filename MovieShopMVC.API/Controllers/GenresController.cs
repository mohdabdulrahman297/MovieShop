using Microsoft.AspNetCore.Mvc;
using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Models.Request;

namespace MovieShopMVC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController(IGenreService genreService) : ControllerBase
    {
        // GET /api/genres?pageNumber=1&pageSize=3
        [HttpGet]
        public async Task<IActionResult> GetGenres(int pageNumber = 1, int pageSize = 3)
        {
            var genres = await genreService.GetPage(pageNumber, pageSize);
            return Ok(genres);
        }

        // POST /api/genres
        [HttpPost]
        public async Task<IActionResult> AddGenre([FromBody] GenreRequest genreRequest)
        {
            await genreService.AddGenre(genreRequest);
            return Created("", new { message = "Genre added successfully." });
        }
    }
}