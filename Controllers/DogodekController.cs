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
    //[Authorize(Roles = "Administrator, Drustvenik")]
    public class DogodekController : Controller
    {
        private readonly Cebelarstvo _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DogodekController(Cebelarstvo context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Dogodek
        public async Task<IActionResult> Index()
        {
            var cebelarstvo = _context.Dogodek.Include(d => d.Drustvo);
            return View(await cebelarstvo.ToListAsync());
        }

        // GET: Dogodek/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Dogodek == null)
            {
                return NotFound();
            }

            var dogodek = await _context.Dogodek
                .Include(d => d.Drustvo)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dogodek == null)
            {
                return NotFound();
            }

            return View(dogodek);
        }

        // GET: Dogodek/Create
        [Authorize(Roles = "Administrator, Drustvenik")]
        public async Task<IActionResult>  Create()
        {
            var id = await GetLoggedInUserDrustvoId();
            ViewData["DrustvoId"] = id;
            ViewData["Drustvo"] = GetDrustvoById(id ?? -1).Result;
            return View();
        }

        // POST: Dogodek/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator, Drustvenik")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Naziv,Lokacija,Opis,Drustvo,DrustvoId")] Dogodek dogodek)
        {
            
            
            if (ModelState.IsValid)
            {
                var res = GetLoggedInUserDrustvoId().Result;
                dogodek.Drustvo = GetDrustvoById(res ?? -1).Result;
                _context.Add(dogodek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } else
            {
            foreach (var key in ModelState.Keys)
                {
                    foreach (var error in ModelState[key].Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            //ViewData["DrustvoId"] = new SelectList(_context.Drustvo, "ID", "ID", dogodek.DrustvoId);
            return View(dogodek);
        }

        // GET: Dogodek/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Dogodek == null)
            {
                return NotFound();
            }

            var dogodek = await _context.Dogodek.FindAsync(id);
            if (dogodek == null)
            {
                return NotFound();
            }
            ViewData["DrustvoId"] = new SelectList(_context.Drustvo, "ID", "ID", dogodek.DrustvoId);
            return View(dogodek);
        }

        // POST: Dogodek/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Naziv,Lokacija,Opis,DrustvoId")] Dogodek dogodek)
        {
            if (id != dogodek.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dogodek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DogodekExists(dogodek.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DrustvoId"] = new SelectList(_context.Drustvo, "ID", "ID", dogodek.DrustvoId);
            return View(dogodek);
        }

        // GET: Dogodek/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dogodek == null)
            {
                return NotFound();
            }

            var dogodek = await _context.Dogodek
                .Include(d => d.Drustvo)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dogodek == null)
            {
                return NotFound();
            }

            return View(dogodek);
        }

        // POST: Dogodek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dogodek == null)
            {
                return Problem("Entity set 'Cebelarstvo.Dogodek'  is null.");
            }
            var dogodek = await _context.Dogodek.FindAsync(id);
            if (dogodek != null)
            {
                _context.Dogodek.Remove(dogodek);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DogodekExists(int id)
        {
          return (_context.Dogodek?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        public async Task<int?> GetLoggedInUserDrustvoId()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return null;
            }

            return user.DrustvoId;
        }
        public async Task<Drustvo> GetDrustvoById(int drustvoId)
        {
            // Find the Drustvo with the specified ID in the database
            var drustvo = await _context.Drustvo.FirstOrDefaultAsync(d => d.Id == drustvoId);

            // If the Drustvo was not found, return null
            if (drustvo == null)
            {
                return null;
            }

            // Otherwise, return the Drustvo object
            return drustvo;
        }
    }
}
