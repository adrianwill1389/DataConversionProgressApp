using System.Diagnostics;
using System.Globalization;
using DataConversionProgressApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataConversionProgressApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var months = Enumerable.Range(1, 12)
                .Select(m => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m))
                .ToList();

            // Optionally: you can pass in progress flags for each month from your DB later
            ViewBag.Months = months;
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Save(List<CourtProgress> model)
        {
            // Simulate saving the data (e.g., update DB or log it)

            TempData["SaveMessage"] = "Saved successfully!";
            return RedirectToAction("Index");
        }


    }
}
