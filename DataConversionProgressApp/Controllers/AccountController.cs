using Microsoft.AspNetCore.Mvc;
using DataConversionProgressApp.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace DataConversionProgressApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.UserAccounts
                .FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

            if (user != null)
            {
                // ✅ Store username in session for use in Save method and UI
                HttpContext.Session.SetString("Username", user.Username);

                // Optional: you could also store user ID or role if needed
                // HttpContext.Session.SetInt32("UserId", user.Id);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Incorrect username or password. Please try again.");
            return View(model);
        }


        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(LoginViewModel model)
        {
            var exists = _context.UserAccounts.Any(u => u.Username == model.Username);
            if (!exists)
            {
                var user = new UserAccount
                {
                    Username = model.Username,
                    Password = model.Password // 🔒 We'll add hashing next!
                };
                _context.UserAccounts.Add(user);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "🎉 Congratulations, you are now registered!";
                return RedirectToAction("Login");
            }

            ViewBag.Error = "Username already exists.";
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}


