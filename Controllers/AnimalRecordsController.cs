using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZooAPI.Models;

namespace ZooAPI.Controllers
{
    [Route("api/[controller]")]
    public class AnimalRecordsController : Controller
    {
        private readonly ZooDbContext _context;

        public AnimalRecordsController(ZooDbContext context)
        {
            _context = context;
        }

        // GET: AnimalRecords
        [HttpGet ("all", Name = "GetAllAnimalRecords")]
        public async Task<IActionResult> GetAllAnimals()
        {
            try
            {
                var animals = await _context.AnimalRecords.ToListAsync();
                return Ok(animals);
            }
            catch (Exception e)
            {
                return BadRequest(new { Error = e.Message, InnerException = e.InnerException?.Message });
            }
        }


        // GET: AnimalRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalRecord = await _context.AnimalRecords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animalRecord == null)
            {
                return NotFound();
            }

            return View(animalRecord);
        }

        // GET: AnimalRecords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnimalRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create", Name = "CreateAnimalRecord")]
        public async Task<IActionResult> Create([FromBody] AnimalRecord animalRecord)
        {
            try
            {
                //_context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Animal_record ON");

                _context.AnimalRecords.Add(animalRecord);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Animal record created successfully" });
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error creating AnimalRecord: {e.Message}, InnerException: {e.InnerException?.Message}");

                return BadRequest(new { Error = e.Message, InnerException = e.InnerException?.Message });
            }
        }


        // GET: api/AnimalRecords/edit/5
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalRecord = await _context.AnimalRecords.FindAsync(id);

            if (animalRecord == null)
            {
                return NotFound();
            }

            return Ok(animalRecord);
        }

        // PUT: AnimalRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("edit/{id}")]
        public async Task<ActionResult<AnimalRecord>> Edit(int id, [FromBody] AnimalRecord animalRecord)
        {
            if (id != animalRecord.Id)
            {
                return NotFound("Animal record not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                  _context.AnimalRecords.Update(animalRecord);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Animal record updated successfully" });
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log the concurrency conflict
                Console.WriteLine("Concurrency conflict. The record has been modified by another user.");

                if (!AnimalRecordExists(id))
                {
                    return NotFound("Animal record not found");
                }
                else
                {
                    return StatusCode(409, "Concurrency conflict. The record has been modified by another user.");
                }
            }
        }






        // GET: AnimalRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalRecord = await _context.AnimalRecords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animalRecord == null)
            {
                return NotFound();
            }

            return View(animalRecord);
        }

        [HttpDelete("delete/{id}", Name = "DeleteAnimalRecord")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var animal = await _context.AnimalRecords.FindAsync(id);

                if (animal == null)
                {
                    return NotFound(new { Message = "Animal not found" });
                }

                _context.AnimalRecords.Remove(animal);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Animal deleted successfully" });
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error deleting AnimalRecord: {e.Message}, InnerException: {e.InnerException?.Message}");
                return BadRequest(new { Error = "Error deleting animal", InnerException = e.InnerException?.Message });
            }
        }

        private bool AnimalRecordExists(int id)
        {
            return _context.AnimalRecords.Any(e => e.Id == id);
        }
    }
}
