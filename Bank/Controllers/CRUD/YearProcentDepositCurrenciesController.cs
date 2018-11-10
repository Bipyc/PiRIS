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
    public class YearProcentDepositCurrenciesController : ControllerBase
    {
        private readonly YearProcentDepositCurrencyContext _context;

        public YearProcentDepositCurrenciesController(YearProcentDepositCurrencyContext context)
        {
            _context = context;
        }

        // GET: api/YearProcentDepositCurrencies
        [HttpGet]
        public IEnumerable<YearProcentDepositCurrency> GetYearProcentDepositCurrencies()
        {
            return _context.YearProcentDepositCurrencies;
        }

        // GET: api/YearProcentDepositCurrencies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetYearProcentDepositCurrency([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var yearProcentDepositCurrency = await _context.YearProcentDepositCurrencies.FindAsync(id);

            if (yearProcentDepositCurrency == null)
            {
                return NotFound();
            }

            return Ok(yearProcentDepositCurrency);
        }

        // PUT: api/YearProcentDepositCurrencies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutYearProcentDepositCurrency([FromRoute] int id, [FromBody] YearProcentDepositCurrency yearProcentDepositCurrency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != yearProcentDepositCurrency.Id)
            {
                return BadRequest();
            }

            _context.Entry(yearProcentDepositCurrency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YearProcentDepositCurrencyExists(id))
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

        // POST: api/YearProcentDepositCurrencies
        [HttpPost]
        public async Task<IActionResult> PostYearProcentDepositCurrency([FromBody] YearProcentDepositCurrency yearProcentDepositCurrency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.YearProcentDepositCurrencies.Add(yearProcentDepositCurrency);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetYearProcentDepositCurrency", new { id = yearProcentDepositCurrency.Id }, yearProcentDepositCurrency);
        }

        // DELETE: api/YearProcentDepositCurrencies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteYearProcentDepositCurrency([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var yearProcentDepositCurrency = await _context.YearProcentDepositCurrencies.FindAsync(id);
            if (yearProcentDepositCurrency == null)
            {
                return NotFound();
            }

            _context.YearProcentDepositCurrencies.Remove(yearProcentDepositCurrency);
            await _context.SaveChangesAsync();

            return Ok(yearProcentDepositCurrency);
        }

        private bool YearProcentDepositCurrencyExists(int id)
        {
            return _context.YearProcentDepositCurrencies.Any(e => e.Id == id);
        }
    }
}