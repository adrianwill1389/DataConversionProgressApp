using Microsoft.AspNetCore.Mvc;
using DataConversionProgressApp.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

public class AccountController : Controller
{
    // Temporary in-memory user list
    private static List<LoginViewModel> users = new List<LoginViewModel>
    {
        new LoginViewModel { Username = "kwhite", Password = "123" }
    };

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        var user = users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
        if (user != null)
        {
            HttpContext.Session.SetString("Username", user.Username);
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, "Incorrect username or password. Please try again.");
        return View(model);
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    [HttpPost]
    public IActionResult Register(LoginViewModel model)
    {
        if (!users.Any(u => u.Username == model.Username))
        {
            users.Add(model);
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
