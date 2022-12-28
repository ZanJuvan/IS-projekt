using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BeeOrganizer.Models;
using Microsoft.AspNetCore.Identity;

namespace BeeOrganizer.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        ViewData["drustvoId"] = GetDrustvoId().Result;
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

    public  async Task<int?> GetDrustvoId()
    {
        int? drustvoId = 0;
        var uporabnik = await _userManager.GetUserAsync(User);
        if (uporabnik != null)
        {
            if (uporabnik.DrustvoId == null ) drustvoId = -2;
            else drustvoId = uporabnik.DrustvoId;
        } else
        {
            drustvoId = -1;
        }
        
        return drustvoId;
    }

     
}
