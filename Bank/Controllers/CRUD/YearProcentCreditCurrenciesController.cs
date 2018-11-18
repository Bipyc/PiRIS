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
    public class YearProcentCreditCurrenciesController : ControllerBase
    {
        private readonly YearProcentCreditCurrencyContext _context;

        public YearProcentCreditCurrenciesController(YearProcentCreditCurrencyContext context)
        {
            _context = context;
        }

        // GET: api/YearProcentCreditCurrencies
        [HttpGet]
        public IEnumerable<YearProcentCreditCurrency> GetYearProcentCreditCurrencies()
        {
            return _context.YearProcentCreditCurrencies;
        }

        // GET: api/YearProcentCreditCurrencies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetYearProcentCreditCurrency([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var yearProcentCreditCurrency = await _context.YearProcentCreditCurrencies.FindAsync(id);

            if (yearProcentCreditCurrency == null)
            {
                return NotFound();
            }

            return Ok(yearProcentCreditCurrency);
        }

        // PUT: api/YearProcentCreditCurrencies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutYearProcentCreditCurrency([FromRoute] int id, [FromBody] YearProcentCreditCurrency yearProcentCreditCurrency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != yearProcentCreditCurrency.Id)
            {
                return BadRequest();
            }

            _context.Entry(yearProcentCreditCurrency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YearProcentCreditCurrencyExists(id))
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

        // POST: api/YearProcentCreditCurrencies
        [HttpPost]
        public async Task<IActionResult> PostYearProcentCreditCurrency([FromBody] YearProcentCreditCurrency yearProcentCreditCurrency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.YearProcentCreditCurrencies.Add(yearProcentCreditCurrency);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetYearProcentCreditCurrency", new { id = yearProcentCreditCurrency.Id }, yearProcentCreditCurrency);
        }

        // DELETE: api/YearProcentCreditCurrencies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteYearProcentCreditCurrency([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var yearProcentCreditCurrency = await _context.YearProcentCreditCurrencies.FindAsync(id);
            if (yearProcentCreditCurrency == null)
            {
                return NotFound();
            }

            _context.YearProcentCreditCurrencies.Remove(yearProcentCreditCurrency);
            await _context.SaveChangesAsync();

            return Ok(yearProcentCreditCurrency);
        }

        private bool YearProcentCreditCurrencyExists(int id)
        {
            return _context.YearProcentCreditCurrencies.Any(e => e.Id == id);
        }
    }
}