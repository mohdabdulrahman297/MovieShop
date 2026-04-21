using Microsoft.AspNetCore.Mvc;
using MovieShop.ApplicationCore.Contracts.Services;
using System.Threading.Tasks;

namespace MovieShop.UI.Controllers
{
    public class CastsController : Controller
    {
        private readonly ICastService _castService;

        public CastsController(ICastService castService)
        {
            _castService = castService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _castService.GetCastDetails(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}