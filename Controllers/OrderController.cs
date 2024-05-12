using Microsoft.AspNetCore.Mvc;

namespace InForno.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
