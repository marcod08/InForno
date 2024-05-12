using Microsoft.AspNetCore.Mvc;

namespace InForno.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
