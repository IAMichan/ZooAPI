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
    public class VeterinaryRecordsController : Controller
    {
        private readonly ZooDbContext _context;

        public VeterinaryRecordsController(ZooDbContext context)
        {
            _context = context;
        }

        // GET: VeterinaryRecords
        public async Task<IActionResult> Index()
        {
            var zooDbContext = _context.VeterinaryRecords.Include(v => v.Animal);
            return View(await zooDbContext.ToListAsync());
        }

        // GET: VeterinaryRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinaryRecord = await _context.VeterinaryRecords
                .Include(v => v.Animal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veterinaryRecord == null)
            {
                return NotFound();
            }

            return View(veterinaryRecord);
        }

        // GET: VeterinaryRecords/Create
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.AnimalRecords, "Id", "Id");
            return View();
        }

        // POST: VeterinaryRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AnimalId,VetId,Date,Diagnosis,Prescription")] VeterinaryRecord veterinaryRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(veterinaryRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(_context.AnimalRecords, "Id", "Id", veterinaryRecord.AnimalId);
            return View(veterinaryRecord);
        }

        // GET: VeterinaryRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinaryRecord = await _context.VeterinaryRecords.FindAsync(id);
            if (veterinaryRecord == null)
            {
                return NotFound();
            }
            ViewData["AnimalId"] = new SelectList(_context.AnimalRecords, "Id", "Id", veterinaryRecord.AnimalId);
            return View(veterinaryRecord);
        }

        // POST: VeterinaryRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AnimalId,VetId,Date,Diagnosis,Prescription")] VeterinaryRecord veterinaryRecord)
        {
            if (id != veterinaryRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veterinaryRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeterinaryRecordExists(veterinaryRecord.Id))
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
            ViewData["AnimalId"] = new SelectList(_context.AnimalRecords, "Id", "Id", veterinaryRecord.AnimalId);
            return View(veterinaryRecord);
        }

        // GET: VeterinaryRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinaryRecord = await _context.VeterinaryRecords
                .Include(v => v.Animal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veterinaryRecord == null)
            {
                return NotFound();
            }

            return View(veterinaryRecord);
        }

        // POST: VeterinaryRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veterinaryRecord = await _context.VeterinaryRecords.FindAsync(id);
            if (veterinaryRecord != null)
            {
                _context.VeterinaryRecords.Remove(veterinaryRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeterinaryRecordExists(int id)
        {
            return _context.VeterinaryRecords.Any(e => e.Id == id);
        }
    }
}
