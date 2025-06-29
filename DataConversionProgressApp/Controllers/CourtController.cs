using Microsoft.AspNetCore.Mvc;
using DataConversionProgressApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using DataConversionProgressApp.Models;

public class CourtController : Controller
{
    private readonly ApplicationDbContext _context;

    public CourtController(ApplicationDbContext context)
    {
        _context = context;
    }

    // 👇 This method runs first when you open the page
    public IActionResult Index(string courtType = "Day", int? month = null, int? year = null)
    {

        var username = HttpContext.Session.GetString("Username");
        if (string.IsNullOrEmpty(username))
        {
            return RedirectToAction("Login", "Account");
        }

        var currentDate = DateTime.Now;

        int selectedMonth = month ?? currentDate.Month;
        int selectedYear = year ?? currentDate.Year;

        var startDate = new DateTime(selectedYear, selectedMonth, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        var dates = GetWorkingDays(startDate, endDate);

        var model = dates.Select(date => new CourtProgress
        {
            DateReceived = date
        }).ToList();

        // Send dropdown values to the View
        ViewBag.SelectedMonth = selectedMonth;
        ViewBag.SelectedYear = selectedYear;
        ViewBag.CourtType = courtType;

        return View(model);
    }

    // 👇 This runs when you press the Save button on the page
    [HttpPost]
    public IActionResult Save(List<CourtProgress> model, string courtType, int month, int year)
    {
        var username = HttpContext.Session.GetString("Username");

        foreach (var item in model)
        {
            var record = new CourtProgressRecord
            {
                DateReceived = item.DateReceived,
                CourtType = courtType,

                Court1Disposed = item.Court1Disposed,
                Court1DisposedBy = item.Court1Disposed ? username : null,

                Court1Warrant = item.Court1Warrant,
                Court1WarrantBy = item.Court1Warrant ? username : null,

                UpdatedBy = username,
                LastUpdated = DateTime.Now
            };

            _context.CourtProgressRecords.Add(record);
        }

        _context.SaveChanges();

        TempData["SaveMessage"] = "✔ Saved Successfully!";
        return RedirectToAction("Index", new { courtType = courtType, month = month, year = year });
    }




    // 👇 THIS is where you paste the GetWorkingDays method (exactly as you wrote it)
    private List<DateTime> GetWorkingDays(DateTime start, DateTime end)
    {
        var holidays = new List<DateTime>
        {
            new DateTime(2025, 1, 1),  // New Year's Day
            new DateTime(2025, 4, 18), // Good Friday
            new DateTime(2025, 4, 21), // Easter Monday
            new DateTime(2025, 5, 23), // Labour Day
            new DateTime(2025, 8, 1),  // Emancipation Day
            new DateTime(2025, 8, 6),  // Independence Day
            new DateTime(2025, 10, 21), // Heroes Day
            new DateTime(2025, 12, 25), // Christmas
            new DateTime(2025, 12, 26), // Boxing Day
        };

        var dates = new List<DateTime>();
        for (var dt = start; dt <= end; dt = dt.AddDays(1))
        {
            if (dt.DayOfWeek != DayOfWeek.Saturday &&
                dt.DayOfWeek != DayOfWeek.Sunday &&
                !holidays.Contains(dt))
            {
                dates.Add(dt);
            }
        }
        return dates;
    }
}
