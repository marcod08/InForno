using InForno.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InForno.Controllers
{
    public class AuthController : Controller
    {
        private readonly InFornoContext _db;

        public AuthController(InFornoContext db)
        {
            _db = db;
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            var loggedUser = await _db.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (loggedUser == null || !BCrypt.Net.BCrypt.Verify(user.Password, loggedUser.Password))
            {
                TempData["ErrorLogin"] = true;
                return RedirectToAction("Login");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, loggedUser.Id.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.Username == newUser.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "Username già in uso. Scegli un altro username.");
                    return View(newUser);
                }

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

                newUser.Password = hashedPassword;

                _db.Users.Add(newUser);
                await _db.SaveChangesAsync();

                TempData["RegistrationSuccess"] = true;
                return RedirectToAction("Login");
            }

            return View(newUser);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
