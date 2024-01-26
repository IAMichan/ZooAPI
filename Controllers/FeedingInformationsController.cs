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
    public class FeedingInformationsController : Controller
    {
        private readonly ZooDbContext _context;

        public FeedingInformationsController(ZooDbContext context)
        {
            _context = context;
        }

        // GET: FeedingInformations
        public async Task<IActionResult> Index()
        {
            var zooDbContext = _context.FeedingInformations.Include(f => f.Animal);
            return View(await zooDbContext.ToListAsync());
        }

        // GET: FeedingInformations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedingInformation = await _context.FeedingInformations
                .Include(f => f.Animal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedingInformation == null)
            {
                return NotFound();
            }

            return View(feedingInformation);
        }

        // GET: FeedingInformations/Create
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.AnimalRecords, "Id", "Id");
            return View();
        }

        // POST: FeedingInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AnimalId,HeadkeeperId,FeedingDateId")] FeedingInformation feedingInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedingInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(_context.AnimalRecords, "Id", "Id", feedingInformation.AnimalId);
            return View(feedingInformation);
        }

        // GET: FeedingInformations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedingInformation = await _context.FeedingInformations.FindAsync(id);
            if (feedingInformation == null)
            {
                return NotFound();
            }
            ViewData["AnimalId"] = new SelectList(_context.AnimalRecords, "Id", "Id", feedingInformation.AnimalId);
            return View(feedingInformation);
        }

        // POST: FeedingInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AnimalId,HeadkeeperId,FeedingDateId")] FeedingInformation feedingInformation)
        {
            if (id != feedingInformation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feedingInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedingInformationExists(feedingInformation.Id))
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
            ViewData["AnimalId"] = new SelectList(_context.AnimalRecords, "Id", "Id", feedingInformation.AnimalId);
            return View(feedingInformation);
        }

        // GET: FeedingInformations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedingInformation = await _context.FeedingInformations
                .Include(f => f.Animal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedingInformation == null)
            {
                return NotFound();
            }

            return View(feedingInformation);
        }

        // POST: FeedingInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feedingInformation = await _context.FeedingInformations.FindAsync(id);
            if (feedingInformation != null)
            {
                _context.FeedingInformations.Remove(feedingInformation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedingInformationExists(int id)
        {
            return _context.FeedingInformations.Any(e => e.Id == id);
        }
    }
}
