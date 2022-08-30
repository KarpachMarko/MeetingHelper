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
    public class RequirementOptionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RequirementOptionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RequirementOptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequirementOption>>> GetRequirementOptions()
        {
          if (_context.RequirementOptions == null)
          {
              return NotFound();
          }
            return await _context.RequirementOptions.ToListAsync();
        }

        // GET: api/RequirementOptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequirementOption>> GetRequirementOption(Guid id)
        {
          if (_context.RequirementOptions == null)
          {
              return NotFound();
          }
            var requirementOption = await _context.RequirementOptions.FindAsync(id);

            if (requirementOption == null)
            {
                return NotFound();
            }

            return requirementOption;
        }

        // PUT: api/RequirementOptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequirementOption(Guid id, RequirementOption requirementOption)
        {
            if (id != requirementOption.Id)
            {
                return BadRequest();
            }

            _context.Entry(requirementOption).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequirementOptionExists(id))
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

        // POST: api/RequirementOptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RequirementOption>> PostRequirementOption(RequirementOption requirementOption)
        {
          if (_context.RequirementOptions == null)
          {
              return Problem("Entity set 'AppDbContext.RequirementOptions'  is null.");
          }
            _context.RequirementOptions.Add(requirementOption);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequirementOption", new { id = requirementOption.Id }, requirementOption);
        }

        // DELETE: api/RequirementOptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequirementOption(Guid id)
        {
            if (_context.RequirementOptions == null)
            {
                return NotFound();
            }
            var requirementOption = await _context.RequirementOptions.FindAsync(id);
            if (requirementOption == null)
            {
                return NotFound();
            }

            _context.RequirementOptions.Remove(requirementOption);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequirementOptionExists(Guid id)
        {
            return (_context.RequirementOptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
