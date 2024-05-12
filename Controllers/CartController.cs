using Microsoft.AspNetCore.Mvc;

namespace InForno.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
