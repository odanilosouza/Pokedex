using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pokedex.Models;

namespace Pokedex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokeControllerAPI : ControllerBase
    {
        private readonly MvcPokeContext _context;

        public PokeControllerAPI(MvcPokeContext context)
        {
            _context = context;
        }

        // GET: api/PokeControllerAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PokeModel>>> GetPokeModel()
        {
          if (_context.PokeModel == null)
          {
              return NotFound();
          }
            return await _context.PokeModel.ToListAsync();
        }

        // GET: api/PokeControllerAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PokeModel>> GetPokeModel(int id)
        {
          if (_context.PokeModel == null)
          {
              return NotFound();
          }
            var pokeModel = await _context.PokeModel.FindAsync(id);

            if (pokeModel == null)
            {
                return NotFound();
            }

            return pokeModel;
        }

        // PUT: api/PokeControllerAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPokeModel(int id, PokeModel pokeModel)
        {
            if (id != pokeModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(pokeModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PokeModelExists(id))
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

        // POST: api/PokeControllerAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PokeModel>> PostPokeModel(PokeModel pokeModel)
        {
          if (_context.PokeModel == null)
          {
              return Problem("Entity set 'MvcPokeContext.PokeModel'  is null.");
          }
            _context.PokeModel.Add(pokeModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPokeModel", new { id = pokeModel.Id }, pokeModel);
        }

        // DELETE: api/PokeControllerAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePokeModel(int id)
        {
            if (_context.PokeModel == null)
            {
                return NotFound();
            }
            var pokeModel = await _context.PokeModel.FindAsync(id);
            if (pokeModel == null)
            {
                return NotFound();
            }

            _context.PokeModel.Remove(pokeModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PokeModelExists(int id)
        {
            return (_context.PokeModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
