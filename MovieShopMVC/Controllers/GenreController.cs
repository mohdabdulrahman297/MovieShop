using Microsoft.AspNetCore.Mvc;
using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Helper;                    
using MovieShop.ApplicationCore.Models.Request;            
using MovieShop.ApplicationCore.Models.Response;           
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        public async Task<IActionResult> Index(int id = 1)
        {
            Page<GenreResponse> result = await _genreService.GetPage(id); 
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(GenreRequest genreRequest) 
        {
            if (ModelState.IsValid)
            {
                await _genreService.AddGenre(genreRequest);   
                return RedirectToAction("Index");
            }
            return View(genreRequest);
        }
    }
}
