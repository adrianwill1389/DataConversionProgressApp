using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataConversionProgressApp; 
using DataConversionProgressApp.Models; // For TickedItem

namespace DataConversionProgressApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult MyReport()
        {
            var username = HttpContext.Session.GetString("Username");

            var activeTicks = _context.TickedItems
                .Where(t => t.Username == username && !t.TaskName.Contains("(Removed)"))
                .OrderByDescending(t => t.Timestamp)
                .ToList();

            var removedTicks = _context.TickedItems
                .Where(t => t.Username == username && t.TaskName.Contains("(Removed)"))
                .OrderByDescending(t => t.Timestamp)
                .ToList();

            ViewBag.RemovedTicks = removedTicks;
            return View(activeTicks);
        }

    }
}


