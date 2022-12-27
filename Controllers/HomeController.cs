﻿using System.Diagnostics;
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
        var drustvoId = GetDrustvoIdAsync();
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

    public async Task<int?> GetDrustvoIdAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        var drustvoId = user.DrustvoId;
        if (drustvoId == null ) drustvoId = 0;
        return drustvoId;
    }
}
