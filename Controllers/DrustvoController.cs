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
            ViewData["userId"] = GetUserIdAsync();
            return View();
        }

        // POST: Dogodek/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Naziv,Lokacija,Opis,DrustvoId")] Dogodek dogodek)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(dogodek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DrustvoId"] = new SelectList(_context.Drustvo, "ID", "ID", dogodek.DrustvoId);
            return View(dogodek);
        }
        public async Task<string> GetUserIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var id = user.Id;
            return id;
        }
    }

    
}