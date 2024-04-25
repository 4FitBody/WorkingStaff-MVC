namespace Just4Fit_WorkingStaff.Presentation.Controllers;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Just4Fit_WorkingStaff.Presentation.Models;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return base.View();
    }

    [HttpGet]
    public IActionResult AboutUs()
    {
        return base.View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
