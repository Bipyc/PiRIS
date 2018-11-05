using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bank.DataManagement.Contexts;
using Bank.Models;
using Bank.Common;
using Microsoft.AspNetCore.Cors;

namespace Bank.Controllers.CRUD
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class CitizenshipsController : ControllerBase
    {
        private readonly CitizenshipContext _context;

        public CitizenshipsController(CitizenshipContext context)
        {
            _context = context;
        }

        // GET: api/Citizenships
        [HttpGet]
        public IEnumerable<Citizenship> GetCitizenships()
        {
            return _context.Citizenships;
        }

        // GET: api/Citizenships/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCitizenship([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var citizenship = await _context.Citizenships.FindAsync(id);

            if (citizenship == null)
            {
                return NotFound();
            }

            return Ok(citizenship);
        }

        // PUT: api/Citizenships/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCitizenship([FromRoute] int id, [FromBody] Citizenship citizenship)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != citizenship.Id)
            {
                return BadRequest();
            }

            _context.Entry(citizenship).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitizenshipExists(id))
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

        // POST: api/Citizenships
        [HttpPost]
        public async Task<IActionResult> PostCitizenship([FromBody] Citizenship citizenship)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Citizenships.Add(citizenship);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCitizenship", new { id = citizenship.Id }, citizenship);
        }

        // DELETE: api/Citizenships/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCitizenship([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var citizenship = await _context.Citizenships.FindAsync(id);
            if (citizenship == null)
            {
                return NotFound();
            }

            _context.Citizenships.Remove(citizenship);
            await _context.SaveChangesAsync();

            return Ok(citizenship);
        }

        private bool CitizenshipExists(int id)
        {
            return _context.Citizenships.Any(e => e.Id == id);
        }
    }
}