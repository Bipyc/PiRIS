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
    public class DepositsController : ControllerBase
    {
        private readonly DepositContext _context;

        public DepositsController(DepositContext context)
        {
            _context = context;
        }

        // GET: api/Deposits
        [HttpGet]
        public IEnumerable<Deposit> GetDeposits()
        {
            var result = _context.Deposits
                            .Include(deposit => deposit.CurrencyTypes)
                            .ThenInclude(yearProcentDepositCurrency => yearProcentDepositCurrency.CurrencyType);

            return result;
        }

        // GET: api/Deposits/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeposit([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deposit = await _context.Deposits.FindAsync(id);

            if (deposit == null)
            {
                return NotFound();
            }

            return Ok(deposit);
        }

        // PUT: api/Deposits/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeposit([FromRoute] int id, [FromBody] Deposit deposit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deposit.Id)
            {
                return BadRequest();
            }

            _context.Entry(deposit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepositExists(id))
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

        // POST: api/Deposits
        [HttpPost]
        public async Task<IActionResult> PostDeposit([FromBody] Deposit deposit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Deposits.Add(deposit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeposit", new { id = deposit.Id }, deposit);
        }

        // DELETE: api/Deposits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeposit([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deposit = await _context.Deposits.FindAsync(id);
            if (deposit == null)
            {
                return NotFound();
            }

            _context.Deposits.Remove(deposit);
            await _context.SaveChangesAsync();

            return Ok(deposit);
        }

        private bool DepositExists(int id)
        {
            return _context.Deposits.Any(e => e.Id == id);
        }
    }
}