using InForno.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace InForno.Controllers
{
    public class MenuController : Controller
    {
        private readonly InFornoContext _db;

        public MenuController(InFornoContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var pizze = _db.Pizza.ToList();
            return View(pizze);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                pizza.Price = decimal.Parse(pizza.Price.ToString().Replace(".", ","));
                _db.Pizza.Add(pizza);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Pizza aggiunta correttamente.";
                ModelState.Clear();
            }
            return View(pizza);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var pizzaToDelete = _db.Pizza.Find(id);
            if (pizzaToDelete != null)
            {
                _db.Pizza.Remove(pizzaToDelete);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Pizza eliminata correttamente.";
            }
            return RedirectToAction("Index");
        }
    }
}
