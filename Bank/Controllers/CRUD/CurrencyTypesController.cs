using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bank.DataManagement.Contexts;
using Bank.Models;

namespace Bank.Controllers.CRUD
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyTypesController : ControllerBase
    {
        private readonly CurrencyTypeContext _context;

        public CurrencyTypesController(CurrencyTypeContext context)
        {
            _context = context;
        }

        // GET: api/CurrencyTypes
        [HttpGet]
        public IEnumerable<CurrencyType> GetCurrencyTypes()
        {
            return _context.CurrencyTypes;
        }

        // GET: api/CurrencyTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurrencyType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currencyType = await _context.CurrencyTypes.FindAsync(id);

            if (currencyType == null)
            {
                return NotFound();
            }

            return Ok(currencyType);
        }

        // PUT: api/CurrencyTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurrencyType([FromRoute] int id, [FromBody] CurrencyType currencyType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != currencyType.Id)
            {
                return BadRequest();
            }

            _context.Entry(currencyType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyTypeExists(id))
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

        // POST: api/CurrencyTypes
        [HttpPost]
        public async Task<IActionResult> PostCurrencyType([FromBody] CurrencyType currencyType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CurrencyTypes.Add(currencyType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurrencyType", new { id = currencyType.Id }, currencyType);
        }

        // DELETE: api/CurrencyTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrencyType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currencyType = await _context.CurrencyTypes.FindAsync(id);
            if (currencyType == null)
            {
                return NotFound();
            }

            _context.CurrencyTypes.Remove(currencyType);
            await _context.SaveChangesAsync();

            return Ok(currencyType);
        }

        private bool CurrencyTypeExists(int id)
        {
            return _context.CurrencyTypes.Any(e => e.Id == id);
        }
    }
}