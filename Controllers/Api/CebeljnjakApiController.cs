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
    [Route("api/v1/cabeljnjak")]
    [ApiController]
    public class CebeljnjakApiController : ControllerBase
    {
        private readonly Cebelarstvo _context;

        public CebeljnjakApiController(Cebelarstvo context)
        {
            _context = context;
        }

        // GET: api/CebeljnjakApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cebeljnjak>>> GetCebeljnjaki()
        {
          if (_context.Cebeljnjaki == null)
          {
              return NotFound();
          }
            return await _context.Cebeljnjaki.ToListAsync();
        }

        // GET: api/CebeljnjakApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cebeljnjak>> GetCebeljnjak(int id)
        {
          if (_context.Cebeljnjaki == null)
          {
              return NotFound();
          }
            var cebeljnjak = await _context.Cebeljnjaki.FindAsync(id);

            if (cebeljnjak == null)
            {
                return NotFound();
            }

            return cebeljnjak;
        }

        // GET: api/CebeljnjakApi/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Cebeljnjak>>> GetCebeljnjakByUser(String userId)
        {
          if (_context.Cebeljnjaki == null)
          {
              return NotFound();
          }
            var cebeljnjaki = await _context.Cebeljnjaki
                    .Where(c => c.UporabnikId == userId)
                    .ToListAsync();

            if (cebeljnjaki == null)
            {
                return NotFound();
            }

            return cebeljnjaki;
        }

        // PUT: api/CebeljnjakApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCebeljnjak(int id, Cebeljnjak cebeljnjak)
        {
            if (id != cebeljnjak.ID)
            {
                return BadRequest();
            }

            _context.Entry(cebeljnjak).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CebeljnjakExists(id))
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

        // POST: api/CebeljnjakApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cebeljnjak>> PostCebeljnjak(Cebeljnjak cebeljnjak)
        {
          if (_context.Cebeljnjaki == null)
          {
              return Problem("Entity set 'Cebelarstvo.Cebeljnjaki'  is null.");
          }
            _context.Cebeljnjaki.Add(cebeljnjak);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCebeljnjak", new { id = cebeljnjak.ID }, cebeljnjak);
        }

        [HttpPut]
        public async Task<ActionResult<Cebeljnjak>> PostCebeljnjakCustom(string naziv, string userId, int num)
        {
            Cebeljnjak cebeljnjak = new Cebeljnjak();
            if (_context.Cebeljnjaki == null)
            {
                return Problem("Entity set 'Cebelarstvo.Cebeljnjaki'  is null.");
            }
            cebeljnjak.UporabnikId = userId;
            cebeljnjak.Naslov = naziv;
            _context.Cebeljnjaki.Add(cebeljnjak);
            await _context.SaveChangesAsync();


            Panj panj;
            for(int i=0; i< num; i++)
            {
                panj = new Panj();
                panj.Naziv = "Panj " +(i+1);
                panj.CebeljnjakID = cebeljnjak.ID;
                panj.Cebeljnjak =  await _context.Cebeljnjaki.FindAsync(cebeljnjak.ID);
                
                _context.Panji.Add(panj);
            }

            return CreatedAtAction("GetCebeljnjak", new { id = cebeljnjak.ID }, cebeljnjak);
        }

        // DELETE: api/CebeljnjakApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCebeljnjak(int id)
        {
            if (_context.Cebeljnjaki == null)
            {
                return NotFound();
            }
            var cebeljnjak = await _context.Cebeljnjaki.FindAsync(id);
            if (cebeljnjak == null)
            {
                return NotFound();
            }

            _context.Cebeljnjaki.Remove(cebeljnjak);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CebeljnjakExists(int id)
        {
            return (_context.Cebeljnjaki?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
