using InForno.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
            var pizze = _db.Pizze.ToList();
            return View(pizze);
        }

        public IActionResult GetPizze()
        {
            var pizze = _db.Pizze.ToList();
            return PartialView("_Pizze", pizze);
        }

        public IActionResult GetDrinks()
        {
            var drinks = _db.Drinks.ToList();
            return PartialView("_Drinks", drinks);
        }

        [Authorize(Roles = "admin")]
        public IActionResult AddPizza()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddPizza(Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                pizza.Price = decimal.Parse(pizza.Price.ToString().Replace(".", ","));
                _db.Pizze.Add(pizza);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Pizza aggiunta correttamente.";
                ModelState.Clear();
            }
            return View(pizza);
        }

        [Authorize(Roles = "admin")]
        public IActionResult AddDrink()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddDrink(Drink drink)
        {
            if (ModelState.IsValid)
            {
                drink.Price = decimal.Parse(drink.Price.ToString().Replace(".", ","));
                _db.Drinks.Add(drink);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Bevanda aggiunta correttamente.";
                ModelState.Clear();
            }
            return View(drink);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult DeletePizza(int id)
        {
            var pizzaToDelete = _db.Pizze.Find(id);
            if (pizzaToDelete != null)
            {
                _db.Pizze.Remove(pizzaToDelete);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Pizza eliminata correttamente.";
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult DeleteDrink(int id)
        {
            var drinkToDelete = _db.Drinks.Find(id);
            if (drinkToDelete != null)
            {
                _db.Drinks.Remove(drinkToDelete);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Bevanda eliminata correttamente.";
            }
            return RedirectToAction("Index");
        }
    }
}
