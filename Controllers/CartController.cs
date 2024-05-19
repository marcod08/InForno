using InForno.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace InForno.Controllers
{
    public class CartController : Controller
    {
        private readonly InFornoContext _db;

        public CartController(InFornoContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPizzaToCart(int pizzaId)
        {
            var pizza = _db.Pizze.Find(pizzaId);
            if (pizza == null)
            {
                TempData["ErrorMessage"] = "Pizza non trovata";
                return RedirectToAction("Index", "Menu");
            }

            var cart = GetCart();
            cart.Add(new CartItem
            {
                PizzaId = pizza.Id,
                Name = pizza.Name,
                Price = pizza.Price,
                Quantity = 1
            });

            SaveCart(cart);

            TempData["SuccessMessage"] = "Pizza aggiunta al carrello";
            return RedirectToAction("Index", "Menu");
        }

        [HttpPost]
        public IActionResult AddDrinkToCart(int drinkId)
        {
            var drink = _db.Drinks.Find(drinkId);
            if (drink == null)
            {
                TempData["ErrorMessage"] = "Drink non trovato";
                return RedirectToAction("Index", "Menu");
            }

            var cart = GetCart();
            cart.Add(new CartItem
            {
                DrinkId = drink.Id,
                Name = drink.Name,
                Price = drink.Price,
                Quantity = 1
            });

            SaveCart(cart);

            TempData["SuccessMessage"] = "Drink aggiunto al carrello";
            return RedirectToAction("Index", "Menu");
        }


        private List<CartItem> GetCart()
        {
            var cookie = Request.Cookies["cart"];
            if (string.IsNullOrEmpty(cookie))
            {
                return new List<CartItem>();
            }
            var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(cookie);
            return cartItems ?? new List<CartItem>();
        }

        private void SaveCart(List<CartItem> cart)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7)
            };
            Response.Cookies.Append("cart", JsonConvert.SerializeObject(cart), cookieOptions);
        }

    }
}
