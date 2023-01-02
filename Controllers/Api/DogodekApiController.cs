using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeeOrganizer.Data;
using BeeOrganizer.Models;

namespace BeeOrganizer.Controllers_Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogodekApiController : ControllerBase
    {
        private readonly Cebelarstvo _context;

        public DogodekApiController(Cebelarstvo context)
        {
            _context = context;
        }

        // GET: api/DogodekApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dogodek>>> GetDogodek()
        {
          if (_context.Dogodek == null)
          {
              return NotFound();
          }
            return await _context.Dogodek.ToListAsync();
        }

        // GET: api/DogodekApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dogodek>> GetDogodek(int id)
        {
          if (_context.Dogodek == null)
          {
              return NotFound();
          }
            var dogodek = await _context.Dogodek.FindAsync(id);

            if (dogodek == null)
            {
                return NotFound();
            }

            return dogodek;
        }

        // PUT: api/DogodekApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDogodek(int id, Dogodek dogodek)
        {
            if (id != dogodek.ID)
            {
                return BadRequest();
            }

            _context.Entry(dogodek).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DogodekExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DogodekApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dogodek>> PostDogodek(Dogodek dogodek)
        {
          if (_context.Dogodek == null)
          {
              return Problem("Entity set 'Cebelarstvo.Dogodek'  is null.");
          }
            _context.Dogodek.Add(dogodek);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDogodek", new { id = dogodek.ID }, dogodek);
        }

        // DELETE: api/DogodekApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDogodek(int id)
        {
            if (_context.Dogodek == null)
            {
                return NotFound();
            }
            var dogodek = await _context.Dogodek.FindAsync(id);
            if (dogodek == null)
            {
                return NotFound();
            }

            _context.Dogodek.Remove(dogodek);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DogodekExists(int id)
        {
            return (_context.Dogodek?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
