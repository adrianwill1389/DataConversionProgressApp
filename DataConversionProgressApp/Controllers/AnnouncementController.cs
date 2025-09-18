using Microsoft.AspNetCore.Mvc;
using DataConversionProgressApp;
using DataConversionProgressApp.Models; // adjust if your model is elsewhere
using System;
using System.Linq;

public class AnnouncementController : Controller
{
    private readonly ApplicationDbContext _context;

    public AnnouncementController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var announcements = _context.Announcements
            .OrderByDescending(a => a.Timestamp)
            .ToList();
        return View(announcements);
    }

    [HttpPost]
    public IActionResult PostAnnouncement(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            TempData["Error"] = "Message cannot be empty.";
            return RedirectToAction("Index");
        }

        var username = HttpContext.Session.GetString("Username") ?? "System";


        var announcement = new Announcement
        {
            Username = username,
            Message = message,
            Timestamp = DateTime.Now
        };

        _context.Announcements.Add(announcement);
        _context.SaveChanges();

        TempData["Success"] = "Announcement posted successfully.";
        return RedirectToAction("Index");
    }

    public IActionResult ViewLatest()
    {
        var username = HttpContext.Session.GetString("Username");
        var user = _context.UserAccounts.FirstOrDefault(u => u.Username == username);

        var latest = _context.Announcements
            .OrderByDescending(a => a.Timestamp)
            .FirstOrDefault();

        if (user != null && latest != null)
        {
            user.LastSeenAnnouncementTime = DateTime.Now;
            _context.UserAccounts.Update(user);
            _context.SaveChanges();
        }

        return View("AnnouncementDetails", latest); // Or whatever view shows the full message
    }


}

