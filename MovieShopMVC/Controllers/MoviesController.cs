using Microsoft.AspNetCore.Mvc;
using MovieShop.ApplicationCore.Contracts.Services;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetHighestGrossingMovies();
            return View(movies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);

            if (movie == null)
            {
                return Content("Movie not found in database or service returned null");
            }

            return View(movie);
        }

        public async Task<IActionResult> MoviesByGenre(int id, int pageNumber = 1, int pageSize = 30)
        {
            var movies = await _movieService.GetMoviesByGenre(id, pageNumber, pageSize);

            if (movies == null || movies.TotalRecords == 0)
            {
                return Content("No movies found for this genre");
            }

            // Pass genreId to view for pagination links
            ViewBag.GenreId = id;

            return View(movies);
        }
    }
}