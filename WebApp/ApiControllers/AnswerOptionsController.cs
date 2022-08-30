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
    public class AnswerOptionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnswerOptionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AnswerOptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnswerOption>>> GetAnswerOptions()
        {
          if (_context.AnswerOptions == null)
          {
              return NotFound();
          }
            return await _context.AnswerOptions.ToListAsync();
        }

        // GET: api/AnswerOptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnswerOption>> GetAnswerOption(Guid id)
        {
          if (_context.AnswerOptions == null)
          {
              return NotFound();
          }
            var answerOption = await _context.AnswerOptions.FindAsync(id);

            if (answerOption == null)
            {
                return NotFound();
            }

            return answerOption;
        }

        // PUT: api/AnswerOptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswerOption(Guid id, AnswerOption answerOption)
        {
            if (id != answerOption.Id)
            {
                return BadRequest();
            }

            _context.Entry(answerOption).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswerOptionExists(id))
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

        // POST: api/AnswerOptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AnswerOption>> PostAnswerOption(AnswerOption answerOption)
        {
          if (_context.AnswerOptions == null)
          {
              return Problem("Entity set 'AppDbContext.AnswerOptions'  is null.");
          }
            _context.AnswerOptions.Add(answerOption);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnswerOption", new { id = answerOption.Id }, answerOption);
        }

        // DELETE: api/AnswerOptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswerOption(Guid id)
        {
            if (_context.AnswerOptions == null)
            {
                return NotFound();
            }
            var answerOption = await _context.AnswerOptions.FindAsync(id);
            if (answerOption == null)
            {
                return NotFound();
            }

            _context.AnswerOptions.Remove(answerOption);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnswerOptionExists(Guid id)
        {
            return (_context.AnswerOptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
