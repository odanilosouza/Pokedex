using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pokedex.Models;

namespace Pokedex.Controllers
{
    public class PokeController : Controller
    {
        private readonly MvcPokeContext _context;

        public PokeController(MvcPokeContext context)
        {
            _context = context;
        }

        // GET: Poke
        public async Task<IActionResult> Index()
        {
              return _context.PokeModel != null ? 
                          View(await _context.PokeModel.ToListAsync()) :
                          Problem("Entity set 'MvcPokeContext.PokeModel'  is null.");
        }

        // GET: Poke/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PokeModel == null)
            {
                return NotFound();
            }

            var pokeModel = await _context.PokeModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokeModel == null)
            {
                return NotFound();
            }

            return View(pokeModel);
        }

        // GET: Poke/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Poke/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type")] PokeModel pokeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pokeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pokeModel);
        }

        // GET: Poke/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PokeModel == null)
            {
                return NotFound();
            }

            var pokeModel = await _context.PokeModel.FindAsync(id);
            if (pokeModel == null)
            {
                return NotFound();
            }
            return View(pokeModel);
        }

        // POST: Poke/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type")] PokeModel pokeModel)
        {
            if (id != pokeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pokeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokeModelExists(pokeModel.Id))
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
            return View(pokeModel);
        }

        // GET: Poke/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PokeModel == null)
            {
                return NotFound();
            }

            var pokeModel = await _context.PokeModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokeModel == null)
            {
                return NotFound();
            }

            return View(pokeModel);
        }

        // POST: Poke/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PokeModel == null)
            {
                return Problem("Entity set 'MvcPokeContext.PokeModel'  is null.");
            }
            var pokeModel = await _context.PokeModel.FindAsync(id);
            if (pokeModel != null)
            {
                _context.PokeModel.Remove(pokeModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokeModelExists(int id)
        {
          return (_context.PokeModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
