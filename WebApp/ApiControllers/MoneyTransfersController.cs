using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyTransfersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MoneyTransfersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MoneyTransfers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoneyTransfer>>> GetMoneyTransfers()
        {
          if (_context.MoneyTransfers == null)
          {
              return NotFound();
          }
            return await _context.MoneyTransfers.ToListAsync();
        }

        // GET: api/MoneyTransfers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MoneyTransfer>> GetMoneyTransfer(Guid id)
        {
          if (_context.MoneyTransfers == null)
          {
              return NotFound();
          }
            var moneyTransfer = await _context.MoneyTransfers.FindAsync(id);

            if (moneyTransfer == null)
            {
                return NotFound();
            }

            return moneyTransfer;
        }

        // PUT: api/MoneyTransfers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoneyTransfer(Guid id, MoneyTransfer moneyTransfer)
        {
            if (id != moneyTransfer.Id)
            {
                return BadRequest();
            }

            _context.Entry(moneyTransfer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoneyTransferExists(id))
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

        // POST: api/MoneyTransfers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MoneyTransfer>> PostMoneyTransfer(MoneyTransfer moneyTransfer)
        {
          if (_context.MoneyTransfers == null)
          {
              return Problem("Entity set 'AppDbContext.MoneyTransfers'  is null.");
          }
            _context.MoneyTransfers.Add(moneyTransfer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMoneyTransfer", new { id = moneyTransfer.Id }, moneyTransfer);
        }

        // DELETE: api/MoneyTransfers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoneyTransfer(Guid id)
        {
            if (_context.MoneyTransfers == null)
            {
                return NotFound();
            }
            var moneyTransfer = await _context.MoneyTransfers.FindAsync(id);
            if (moneyTransfer == null)
            {
                return NotFound();
            }

            _context.MoneyTransfers.Remove(moneyTransfer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MoneyTransferExists(Guid id)
        {
            return (_context.MoneyTransfers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
