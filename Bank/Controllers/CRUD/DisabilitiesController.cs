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

namespace Bank.Controllers.CRUD
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisabilitiesController : ControllerBase
    {
        private readonly DisabilityContext _context;

        public DisabilitiesController(DisabilityContext context)
        {
            _context = context;
        }

        // GET: api/Disabilities
        [HttpGet]
        public IEnumerable<Disability> GetDisabilities()
        {
            CommonUtils.AddDefaultHeaders(HttpContext);

            return _context.Disabilities;
        }

        // GET: api/Disabilities/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDisability([FromRoute] int id)
        {
            CommonUtils.AddDefaultHeaders(HttpContext);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var disability = await _context.Disabilities.FindAsync(id);

            if (disability == null)
            {
                return NotFound();
            }

            return Ok(disability);
        }

        // PUT: api/Disabilities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDisability([FromRoute] int id, [FromBody] Disability disability)
        {
            CommonUtils.AddDefaultHeaders(HttpContext);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != disability.Id)
            {
                return BadRequest();
            }

            _context.Entry(disability).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DisabilityExists(id))
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

        // POST: api/Disabilities
        [HttpPost]
        public async Task<IActionResult> PostDisability([FromBody] Disability disability)
        {
            CommonUtils.AddDefaultHeaders(HttpContext);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Disabilities.Add(disability);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDisability", new { id = disability.Id }, disability);
        }

        // DELETE: api/Disabilities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDisability([FromRoute] int id)
        {
            CommonUtils.AddDefaultHeaders(HttpContext);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var disability = await _context.Disabilities.FindAsync(id);
            if (disability == null)
            {
                return NotFound();
            }

            _context.Disabilities.Remove(disability);
            await _context.SaveChangesAsync();

            return Ok(disability);
        }

        private bool DisabilityExists(int id)
        {
            return _context.Disabilities.Any(e => e.Id == id);
        }
    }
}