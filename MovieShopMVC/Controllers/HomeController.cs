using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}