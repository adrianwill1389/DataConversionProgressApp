using Microsoft.AspNetCore.Mvc;
using DataConversionProgressApp.Models;
using System.Collections.Generic;
using DataConversionProgressApp.Models;

public class CourtController : Controller
{
    public IActionResult Index()
    {
        var sampleList = new List<CourtProgress>
        {
            new CourtProgress { DateReceived = new DateTime(2025,5,1), Court1Disposed = true, Court1Warrant = true, Court2CaseManagement = true, Court2Warrant = true, User = "Admin" },
            new CourtProgress { DateReceived = new DateTime(2025,5,2), Court1Disposed = true, Court1Warrant = true, Court2CaseManagement = true, Court2Warrant = true, User = "Clerk1" },
            new CourtProgress { DateReceived = new DateTime(2025,5,5), Court1Disposed = true, Court1Warrant = true, Court2CaseManagement = true, Court2Warrant = false, User = "Clerk2" },
            // Add more entries like your Excel
        };

        return View(sampleList);
    }
}
