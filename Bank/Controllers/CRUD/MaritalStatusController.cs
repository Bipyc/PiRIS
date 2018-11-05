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
    public class MaritalStatusController : ControllerBase
    {
        private readonly MaritalStatusContext _context;

        public MaritalStatusController(MaritalStatusContext context)
        {
            _context = context;
        }

        // GET: api/MaritalStatus
        [HttpGet]
        public IEnumerable<MaritalStatus> GetMaritalStatuses()
        {
            return _context.MaritalStatuses;
        }

        // GET: api/MaritalStatus/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaritalStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var maritalStatus = await _context.MaritalStatuses.FindAsync(id);

            if (maritalStatus == null)
            {
                return NotFound();
            }

            return Ok(maritalStatus);
        }

        // PUT: api/MaritalStatus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaritalStatus([FromRoute] int id, [FromBody] MaritalStatus maritalStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != maritalStatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(maritalStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaritalStatusExists(id))
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

        // POST: api/MaritalStatus
        [HttpPost]
        public async Task<IActionResult> PostMaritalStatus([FromBody] MaritalStatus maritalStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MaritalStatuses.Add(maritalStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaritalStatus", new { id = maritalStatus.Id }, maritalStatus);
        }

        // DELETE: api/MaritalStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaritalStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var maritalStatus = await _context.MaritalStatuses.FindAsync(id);
            if (maritalStatus == null)
            {
                return NotFound();
            }

            _context.MaritalStatuses.Remove(maritalStatus);
            await _context.SaveChangesAsync();

            return Ok(maritalStatus);
        }

        private bool MaritalStatusExists(int id)
        {
            return _context.MaritalStatuses.Any(e => e.Id == id);
        }
    }
}