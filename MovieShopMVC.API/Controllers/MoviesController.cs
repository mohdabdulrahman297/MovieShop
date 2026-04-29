using Microsoft.AspNetCore.Mvc;
using MovieShop.ApplicationCore.Contracts.Services;

namespace MovieShopMVC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET /api/movies/highest-grossing
        [HttpGet("highest-grossing")]
        public async Task<IActionResult> GetHighestGrossing()
        {
            var movies = await _movieService.GetHighestGrossingMovies();
            return Ok(movies);
        }

        // GET /api/movies/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMovieDetails(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            if (movie == null)
                return NotFound(new { message = $"Movie with id {id} not found." });
            return Ok(movie);
        }

        // GET /api/movies/genre/{genreId}?pageNumber=1&pageSize=30
        [HttpGet("genre/{genreId:int}")]
        public async Task<IActionResult> GetMoviesByGenre(int genreId, int pageNumber = 1, int pageSize = 30)
        {
            var movies = await _movieService.GetMoviesByGenre(genreId, pageNumber, pageSize);
            return Ok(movies);
        }
    }
}