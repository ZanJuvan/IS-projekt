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
    public class PanjApiController : ControllerBase
    {
        private readonly Cebelarstvo _context;

        public PanjApiController(Cebelarstvo context)
        {
            _context = context;
        }

        // GET: api/PanjApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Panj>>> GetPanji()
        {
            if (_context.Panji == null)
            {
                return NotFound();
            }
            return await _context.Panji.ToListAsync();
        }

        // GET: api/PanjApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Panj>> GetPanj(int id)
        {
            if (_context.Panji == null)
            {
                return NotFound();
            }
            var panj = await _context.Panji.FindAsync(id);

            if (panj == null)
            {
                return NotFound();
            }

            return panj;
        }

        [HttpGet("{cebeljnjakId}")]
        public async Task<ActionResult<IEnumerable<Panj>>> GetPanjbyCebeljnjak(int cebeljnjakId)
        {
            if (_context.Panji == null)
            {
                return NotFound();
            }
            var panj = await _context.Panji
                    .Where(c => c.CebeljnjakID == cebeljnjakId)
                    .ToListAsync();

            if (panj == null)
            {
                return NotFound();
            }

            return panj;
        }


        // PUT: api/PanjApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPanj(int id, Panj panj)
        {
            if (id != panj.PanjID)
            {
                return BadRequest();
            }

            _context.Entry(panj).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PanjExists(id))
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

        // POST: api/PanjApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Panj>> PostPanj(Panj panj)
        {
          if (_context.Panji == null)
          {
              return Problem("Entity set 'Cebelarstvo.Panji'  is null.");
          }
            _context.Panji.Add(panj);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPanj", new { id = panj.PanjID }, panj);
        }

        [HttpPost]
        public async Task<ActionResult<Panj>> PostMorePanjs(int num, int beeHouseId)
        {
          Panj panj;
          for(int i=0; i> num; i++)
          {
            panj = new Panj();
            panj.Naziv = "Panj " +(i+1);
            panj.CebeljnjakID = beeHouseId;
            panj.Cebeljnjak =  await _context.Cebeljnjaki.FindAsync(beeHouseId);
            
            _context.Panji.Add(panj);
          }
            
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/PanjApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePanj(int id)
        {
            if (_context.Panji == null)
            {
                return NotFound();
            }
            var panj = await _context.Panji.FindAsync(id);
            if (panj == null)
            {
                return NotFound();
            }

            _context.Panji.Remove(panj);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PanjExists(int id)
        {
            return (_context.Panji?.Any(e => e.PanjID == id)).GetValueOrDefault();
        }
    }
}
