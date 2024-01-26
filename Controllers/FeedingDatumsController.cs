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
    public class FeedingDatumsController : Controller
    {
        private readonly ZooDbContext _context;

        public FeedingDatumsController(ZooDbContext context)
        {
            _context = context;
        }

        // GET: FeedingDatums
        public async Task<IActionResult> Index()
        {
            var zooDbContext = _context.FeedingData.Include(f => f.Animal);
            return View(await zooDbContext.ToListAsync());
        }

        // GET: FeedingDatums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedingDatum = await _context.FeedingData
                .Include(f => f.Animal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedingDatum == null)
            {
                return NotFound();
            }

            return View(feedingDatum);
        }

        // GET: FeedingDatums/Create
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.AnimalRecords, "Id", "Id");
            return View();
        }

        // POST: FeedingDatums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AnimalId,FromDate,Food")] FeedingDatum feedingDatum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedingDatum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(_context.AnimalRecords, "Id", "Id", feedingDatum.AnimalId);
            return View(feedingDatum);
        }

        // GET: FeedingDatums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedingDatum = await _context.FeedingData.FindAsync(id);
            if (feedingDatum == null)
            {
                return NotFound();
            }
            ViewData["AnimalId"] = new SelectList(_context.AnimalRecords, "Id", "Id", feedingDatum.AnimalId);
            return View(feedingDatum);
        }

        // POST: FeedingDatums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AnimalId,FromDate,Food")] FeedingDatum feedingDatum)
        {
            if (id != feedingDatum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feedingDatum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedingDatumExists(feedingDatum.Id))
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
            ViewData["AnimalId"] = new SelectList(_context.AnimalRecords, "Id", "Id", feedingDatum.AnimalId);
            return View(feedingDatum);
        }

        // GET: FeedingDatums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedingDatum = await _context.FeedingData
                .Include(f => f.Animal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedingDatum == null)
            {
                return NotFound();
            }

            return View(feedingDatum);
        }

        // POST: FeedingDatums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feedingDatum = await _context.FeedingData.FindAsync(id);
            if (feedingDatum != null)
            {
                _context.FeedingData.Remove(feedingDatum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedingDatumExists(int id)
        {
            return _context.FeedingData.Any(e => e.Id == id);
        }
    }
}
