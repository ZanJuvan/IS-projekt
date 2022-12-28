using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeeOrganizer.Data;
using BeeOrganizer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BeeOrganizer.Controllers
{
    [Authorize]
    public class DrustvoController : Controller
    {
        private readonly Cebelarstvo _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DrustvoController(Cebelarstvo context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Dogodek/Create
        public IActionResult Create()
        {
            ViewBag.UserId = GetLoggedInUserId().Result;
            return View();
        }

        // POST: Dogodek/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Naziv,Lokacija,ApplicationUserId")] Drustvo drustvo)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(drustvo);
                await _context.SaveChangesAsync();

                var user = await _userManager.GetUserAsync(HttpContext.User);
                await _userManager.AddToRoleAsync(user, "Drustvenik");
                user.DrustvoId = drustvo.Id;
                await _userManager.UpdateAsync(user);


                return RedirectToAction("Index", "Home");
            }
            
            return View();
        }
        public async Task<string?> GetLoggedInUserId()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return null;
            }

            return user.Id;
        }

    }


    
}