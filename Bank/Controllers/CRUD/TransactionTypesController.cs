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
    public class TransactionTypesController : ControllerBase
    {
        private readonly TransactionTypeContext _context;

        public TransactionTypesController(TransactionTypeContext context)
        {
            _context = context;
        }

        // GET: api/TransactionTypes
        [HttpGet]
        public IEnumerable<TransactionType> GetTransactionTypes()
        {
            return _context.TransactionTypes;
        }

        // GET: api/TransactionTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transactionType = await _context.TransactionTypes.FindAsync(id);

            if (transactionType == null)
            {
                return NotFound();
            }

            return Ok(transactionType);
        }

        // PUT: api/TransactionTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransactionType([FromRoute] int id, [FromBody] TransactionType transactionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transactionType.Id)
            {
                return BadRequest();
            }

            _context.Entry(transactionType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionTypeExists(id))
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

        // POST: api/TransactionTypes
        [HttpPost]
        public async Task<IActionResult> PostTransactionType([FromBody] TransactionType transactionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TransactionTypes.Add(transactionType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransactionType", new { id = transactionType.Id }, transactionType);
        }

        // DELETE: api/TransactionTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transactionType = await _context.TransactionTypes.FindAsync(id);
            if (transactionType == null)
            {
                return NotFound();
            }

            _context.TransactionTypes.Remove(transactionType);
            await _context.SaveChangesAsync();

            return Ok(transactionType);
        }

        private bool TransactionTypeExists(int id)
        {
            return _context.TransactionTypes.Any(e => e.Id == id);
        }
    }
}