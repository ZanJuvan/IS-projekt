using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeeOrganizer.Data;
using BeeOrganizer.Models;


namespace BeeOrganizer.Controllers
{
    public class CebeljnjakController : Controller
    {
        private readonly Cebelarstvo _context;

        public CebeljnjakController(Cebelarstvo context)
        {
            _context = context;
        }

        // GET: Cebeljnjak
        public async Task<IActionResult> Index()
        {
              return _context.Cebeljnjaki != null ? 
                          View(await _context.Cebeljnjaki.ToListAsync()) :
                          Problem("Entity set 'Cebelarstvo.Cebeljnjaki'  is null.");
        }

        // GET: Cebeljnjak/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cebeljnjaki == null)
            {
                return NotFound();
            }

            var cebeljnjak = await _context.Cebeljnjaki
                .FirstOrDefaultAsync(m => m.CebeljnjakID == id);
            if (cebeljnjak == null)
            {
                return NotFound();
            }

            return View(cebeljnjak);
        }

        // GET: Cebeljnjak/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cebeljnjak/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CebeljnjakID,Naslov,Lokacija")] Cebeljnjak cebeljnjak)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cebeljnjak);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cebeljnjak);
        }

        // GET: Cebeljnjak/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cebeljnjaki == null)
            {
                return NotFound();
            }

            var cebeljnjak = await _context.Cebeljnjaki.FindAsync(id);
            if (cebeljnjak == null)
            {
                return NotFound();
            }
            return View(cebeljnjak);
        }

        // POST: Cebeljnjak/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CebeljnjakID,Naslov,Lokacija")] Cebeljnjak cebeljnjak)
        {
            if (id != cebeljnjak.CebeljnjakID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cebeljnjak);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CebeljnjakExists(cebeljnjak.CebeljnjakID))
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
            return View(cebeljnjak);
        }

        // GET: Cebeljnjak/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cebeljnjaki == null)
            {
                return NotFound();
            }

            var cebeljnjak = await _context.Cebeljnjaki
                .FirstOrDefaultAsync(m => m.CebeljnjakID == id);
            if (cebeljnjak == null)
            {
                return NotFound();
            }

            return View(cebeljnjak);
        }

        // POST: Cebeljnjak/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cebeljnjaki == null)
            {
                return Problem("Entity set 'Cebelarstvo.Cebeljnjaki'  is null.");
            }
            var cebeljnjak = await _context.Cebeljnjaki.FindAsync(id);
            if (cebeljnjak != null)
            {
                _context.Cebeljnjaki.Remove(cebeljnjak);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CebeljnjakExists(int id)
        {
          return (_context.Cebeljnjaki?.Any(e => e.CebeljnjakID == id)).GetValueOrDefault();
        }
    }
}
