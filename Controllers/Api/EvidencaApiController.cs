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
    public class EvidencaApiController : ControllerBase
    {
        private readonly Cebelarstvo _context;

        public EvidencaApiController(Cebelarstvo context)
        {
            _context = context;
        }

        // GET: api/EvidencaApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evidenca>>> GetEvidenca()
        {
          if (_context.Evidenca == null)
          {
              return NotFound();
          }
            return await _context.Evidenca.ToListAsync();
        }

        // GET: api/EvidencaApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Evidenca>> GetEvidenca(int id)
        {
          if (_context.Evidenca == null)
          {
              return NotFound();
          }
            var evidenca = await _context.Evidenca.FindAsync(id);

            if (evidenca == null)
            {
                return NotFound();
            }

            return evidenca;
        }

        // PUT: api/EvidencaApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvidenca(int id, Evidenca evidenca)
        {
            if (id != evidenca.ID)
            {
                return BadRequest();
            }

            _context.Entry(evidenca).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvidencaExists(id))
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

        // POST: api/EvidencaApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Evidenca>> PostEvidenca(Evidenca evidenca)
        {
          if (_context.Evidenca == null)
          {
              return Problem("Entity set 'Cebelarstvo.Evidenca'  is null.");
          }
            _context.Evidenca.Add(evidenca);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvidenca", new { id = evidenca.ID }, evidenca);
        }

        // DELETE: api/EvidencaApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvidenca(int id)
        {
            if (_context.Evidenca == null)
            {
                return NotFound();
            }
            var evidenca = await _context.Evidenca.FindAsync(id);
            if (evidenca == null)
            {
                return NotFound();
            }

            _context.Evidenca.Remove(evidenca);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EvidencaExists(int id)
        {
            return (_context.Evidenca?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
