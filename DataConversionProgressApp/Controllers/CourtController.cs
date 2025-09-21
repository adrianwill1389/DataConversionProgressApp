using Microsoft.AspNetCore.Mvc;
using DataConversionProgressApp.Models;

//Did not implement the username tag on each checkbox yet

namespace DataConversionProgressApp.Controllers
{
    public class CourtController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourtController(ApplicationDbContext context)
        {
            _context = context;
        }

     
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

            ViewBag.SelectedMonth = selectedMonth;
            ViewBag.SelectedYear = selectedYear;
            ViewBag.CourtType = courtType;

            return View(model);
        }

        [HttpPost]
        public IActionResult Save(List<CourtProgressRecord> model, string courtType, int month, int year)
        {
            var username = HttpContext.Session.GetString("Username") ?? "System";

            foreach (var item in model)
            {
                var dateReceived = item.DateReceived != default ? item.DateReceived.Date : DateTime.Today;

                var existing = _context.CourtProgressRecords
                    .FirstOrDefault(r => r.DateReceived.Date == dateReceived && r.CourtType == courtType);

                if (existing == null)
                {
                    existing = new CourtProgressRecord
                    {
                        DateReceived = dateReceived,
                        CourtType = courtType
                    };
                    _context.CourtProgressRecords.Add(existing);
                }

                // Court1 Disposed
                if (item.Court1Disposed != existing.Court1Disposed)
                {
                    existing.Court1Disposed = item.Court1Disposed;
                    existing.Court1DisposedBy = item.Court1Disposed ? username : string.Empty;

                    _context.TickedItems.Add(new TickedItem
                    {
                        Username = username,
                        TaskName = item.Court1Disposed ? "Court1 Disposed" : "Court1 Disposed (Removed)",
                        Date = dateReceived,
                        Timestamp = DateTime.Now
                    });
                }

                // Court1 Warrant
                if (item.Court1Warrant != existing.Court1Warrant)
                {
                    existing.Court1Warrant = item.Court1Warrant;
                    existing.Court1WarrantBy = item.Court1Warrant ? username : string.Empty;

                    _context.TickedItems.Add(new TickedItem
                    {
                        Username = username,
                        TaskName = item.Court1Warrant ? "Court1 Warrant" : "Court1 Warrant (Removed)",
                        Date = dateReceived,
                        Timestamp = DateTime.Now
                    });
                }

                // Court2 Disposed
                if (item.Court2Disposed != existing.Court2Disposed)
                {
                    existing.Court2Disposed = item.Court2Disposed;
                    existing.Court2DisposedBy = item.Court2Disposed ? username : string.Empty;
                

                    _context.TickedItems.Add(new TickedItem
                    {
                        Username = username,
                        TaskName = item.Court2Disposed ? "Court2 Disposed" : "Court2 Disposed (Removed)",
                        Date = dateReceived,
                        Timestamp = DateTime.Now
                    });
                }

                // Court2 Warrant
                if (item.Court2Warrant != existing.Court2Warrant)
                {
                    existing.Court2Warrant = item.Court2Warrant;
                    existing.Court2WarrantBy = item.Court2Warrant ? username : string.Empty;

                    _context.TickedItems.Add(new TickedItem
                    {
                        Username = username,
                        TaskName = item.Court2Warrant ? "Court2 Warrant" : "Court2 Warrant (Removed)",
                        Date = dateReceived,
                        Timestamp = DateTime.Now
                    });
                }

                // Court1 Night
                if (item.Court1Night != existing.Court1Night)
                {
                    existing.Court1Night = item.Court1Night;
                    existing.Court1NightBy = item.Court1Night ? username : string.Empty;

                    _context.TickedItems.Add(new TickedItem
                    {
                        Username = username,
                        TaskName = item.Court1Night ? "Court1 Night" : "Court1 Night (Removed)",
                        Date = dateReceived,
                        Timestamp = DateTime.Now
                    });
                }

                // Court2 Night
                if (item.Court2Night != existing.Court2Night)
                {
                    existing.Court2Night = item.Court2Night;
                    existing.Court2NightBy = item.Court2Night ? username : string.Empty;

                    _context.TickedItems.Add(new TickedItem
                    {
                        Username = username,
                        TaskName = item.Court2Night ? "Court2 Night" : "Court2 Night (Removed)",
                        Date = dateReceived,
                        Timestamp = DateTime.Now
                    });
                }

                // Court3 Night
                if (item.Court3Night != existing.Court3Night)
                {
                    existing.Court3Night = item.Court3Night;
                    existing.Court3NightBy = item.Court3Night ? username : string.Empty;

                    _context.TickedItems.Add(new TickedItem
                    {
                        Username = username,
                        TaskName = item.Court3Night ? "Court3 Night" : "Court3 Night (Removed)",
                        Date = dateReceived,
                        Timestamp = DateTime.Now
                    });
                }

                existing.UpdatedBy = username;
                existing.LastUpdated = DateTime.Now;
            }

            _context.SaveChanges();

            TempData["SaveMessage"] = "✔ Saved Successfully!";
            return RedirectToAction("Index", new { courtType, month, year });
        }


        // Return list of working days excluding weekends and holidays
        private List<DateTime> GetWorkingDays(DateTime start, DateTime end)
        {
            var holidays = new List<DateTime>
            {
                new DateTime(start.Year, 1, 1),   
                new DateTime(start.Year, 4, 18), 
                new DateTime(start.Year, 4, 21),  
                new DateTime(start.Year, 5, 23),  
                new DateTime(start.Year, 8, 1),   
                new DateTime(start.Year, 8, 6),   
                new DateTime(start.Year, 10, 21),
                new DateTime(start.Year, 12, 25), 
                new DateTime(start.Year, 12, 26),
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

        // Merge saved DB data with all dates for display and editing
        private List<CourtProgressRecord> MergeSavedDataIntoModel(List<DateTime> dates, string courtType)
        {
            var dateList = dates.Select(d => d.Date).ToList();

            var saved = _context.CourtProgressRecords
                .Where(r => r.CourtType == courtType && dateList.Contains(r.DateReceived.Date))
                .ToList();

            var model = dates.Select(date =>
            {
                var record = saved.FirstOrDefault(r => r.DateReceived.Date == date.Date);

                return new CourtProgressRecord
                {
                    DateReceived = date,

                    CourtType = courtType,

                    Court1Disposed = record?.Court1Disposed ?? false,
                    Court1DisposedBy = record?.Court1DisposedBy,

                    Court1Warrant = record?.Court1Warrant ?? false,
                    Court1WarrantBy = record?.Court1WarrantBy,

                    Court2Disposed = record?.Court2Disposed ?? false,
                    Court2DisposedBy = record?.Court2DisposedBy,

                    Court2Warrant = record?.Court2Warrant ?? false,
                    Court2WarrantBy = record?.Court2WarrantBy,

                    Court1Night = record?.Court1Night ?? false,
                    Court1NightBy = record?.Court1NightBy,

                    Court2Night = record?.Court2Night ?? false,
                    Court2NightBy = record?.Court2NightBy,

                    Court3Night = record?.Court3Night ?? false,
                    Court3NightBy = record?.Court3NightBy,

                    UpdatedBy = record?.UpdatedBy,
                    LastUpdated = record?.LastUpdated ?? DateTime.MinValue
                };
            }).ToList();

            return model;
        }
    }
}
