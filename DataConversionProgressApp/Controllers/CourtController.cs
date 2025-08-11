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

        var model = MergeSavedDataIntoModel(dates, courtType);



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
        if (string.IsNullOrEmpty(username))
        {
            return RedirectToAction("Login", "Account");
        }

        foreach (var item in model)
        {
            bool hasProgress = item.Court1Disposed || item.Court1Warrant ||
                               item.Court2Disposed || item.Court2Warrant ||
                               item.Court1Night || item.Court2Night || item.Court3Night;

            if (!hasProgress)
                continue;

            // ✅ Check if this user already saved for this date and courtType
            bool alreadySaved = _context.CourtProgressRecords.Any(r =>
                r.DateReceived == item.DateReceived &&
                r.CourtType == courtType &&
                r.UpdatedBy == username);

            if (alreadySaved)
                continue;

            var record = new CourtProgressRecord
            {
                DateReceived = item.DateReceived,
                CourtType = courtType,

                Court1Disposed = item.Court1Disposed,
                Court1DisposedBy = item.Court1Disposed ? username : null,

                Court1Warrant = item.Court1Warrant,
                Court1WarrantBy = item.Court1Warrant ? username : null,

                Court2Disposed = item.Court2Disposed,
                Court2DisposedBy = item.Court2Disposed ? username : null,

                Court2Warrant = item.Court2Warrant,
                Court2WarrantBy = item.Court2Warrant ? username : null,

                Court1Night = item.Court1Night,
                Court1NightBy = item.Court1Night ? username : null,

                Court2Night = item.Court2Night,
                Court2NightBy = item.Court2Night ? username : null,

                Court3Night = item.Court3Night,
                Court3NightBy = item.Court3Night ? username : null,

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
    private List<CourtProgress> MergeSavedDataIntoModel(List<DateTime> dates, string courtType)
    {
        var saved = _context.CourtProgressRecords
            .Where(r => r.CourtType == courtType && dates.Contains(r.DateReceived))
            .ToList();

        var model = new List<CourtProgress>();

        foreach (var date in dates)
        {
            var recordsForDate = saved
                .Where(r => r.DateReceived == date && r.CourtType == courtType)
                .ToList();

            if (!recordsForDate.Any())
            {
                Console.WriteLine($"⚠️ No match for date: {date:yyyy-MM-dd} | CourtType: {courtType}");
            }

            var item = new CourtProgress
            {
                DateReceived = date,
                CourtType = courtType,

                // Day court
                Court1Disposed = recordsForDate.Any(r => r.Court1Disposed),
                Court1DisposedBy = string.Join(", ", recordsForDate.Where(r => r.Court1Disposed && !string.IsNullOrEmpty(r.Court1DisposedBy)).Select(r => r.Court1DisposedBy).Distinct()),
                Court1Warrant = recordsForDate.Any(r => r.Court1Warrant),
                Court1WarrantBy = string.Join(", ", recordsForDate.Where(r => r.Court1Warrant && !string.IsNullOrEmpty(r.Court1WarrantBy)).Select(r => r.Court1WarrantBy).Distinct()),
                Court2Disposed = recordsForDate.Any(r => r.Court2Disposed),
                Court2DisposedBy = string.Join(", ", recordsForDate.Where(r => r.Court2Disposed && !string.IsNullOrEmpty(r.Court2DisposedBy)).Select(r => r.Court2DisposedBy).Distinct()),
                Court2Warrant = recordsForDate.Any(r => r.Court2Warrant),
                Court2WarrantBy = string.Join(", ", recordsForDate.Where(r => r.Court2Warrant && !string.IsNullOrEmpty(r.Court2WarrantBy)).Select(r => r.Court2WarrantBy).Distinct()),

                // Night court
                Court1Night = recordsForDate.Any(r => r.Court1Night),
                Court1NightBy = string.Join(", ", recordsForDate.Where(r => r.Court1Night && !string.IsNullOrEmpty(r.Court1NightBy)).Select(r => r.Court1NightBy).Distinct()),
                Court2Night = recordsForDate.Any(r => r.Court2Night),
                Court2NightBy = string.Join(", ", recordsForDate.Where(r => r.Court2Night && !string.IsNullOrEmpty(r.Court2NightBy)).Select(r => r.Court2NightBy).Distinct()),
                Court3Night = recordsForDate.Any(r => r.Court3Night),
                Court3NightBy = string.Join(", ", recordsForDate.Where(r => r.Court3Night && !string.IsNullOrEmpty(r.Court3NightBy)).Select(r => r.Court3NightBy).Distinct())
            };

            model.Add(item);
        }

        return model;
    }



}
