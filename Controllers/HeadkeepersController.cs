using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZooAPI.Models;

namespace ZooAPI.Controllers
{
    public class HeadkeepersController : Controller
    {
        private readonly ZooDbContext _context;

        public HeadkeepersController(ZooDbContext context)
        {
            _context = context;
        }

        // GET: Headkeepers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Headkeepers.ToListAsync());
        }

        // GET: Headkeepers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var headkeeper = await _context.Headkeepers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (headkeeper == null)
            {
                return NotFound();
            }

            return View(headkeeper);
        }

        // GET: Headkeepers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Headkeepers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Headkeeper headkeeper)
        {
            if (ModelState.IsValid)
            {
                _context.Add(headkeeper);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(headkeeper);
        }

        // GET: Headkeepers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var headkeeper = await _context.Headkeepers.FindAsync(id);
            if (headkeeper == null)
            {
                return NotFound();
            }
            return View(headkeeper);
        }

        // POST: Headkeepers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Headkeeper headkeeper)
        {
            if (id != headkeeper.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(headkeeper);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeadkeeperExists(headkeeper.Id))
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
            return View(headkeeper);
        }

        // GET: Headkeepers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var headkeeper = await _context.Headkeepers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (headkeeper == null)
            {
                return NotFound();
            }

            return View(headkeeper);
        }

        // POST: Headkeepers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var headkeeper = await _context.Headkeepers.FindAsync(id);
            if (headkeeper != null)
            {
                _context.Headkeepers.Remove(headkeeper);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeadkeeperExists(int id)
        {
            return _context.Headkeepers.Any(e => e.Id == id);
        }
    }
}
