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
    public class RequirementsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RequirementsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Requirements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Requirement>>> GetRequirements()
        {
          if (_context.Requirements == null)
          {
              return NotFound();
          }
            return await _context.Requirements.ToListAsync();
        }

        // GET: api/Requirements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Requirement>> GetRequirement(Guid id)
        {
          if (_context.Requirements == null)
          {
              return NotFound();
          }
            var requirement = await _context.Requirements.FindAsync(id);

            if (requirement == null)
            {
                return NotFound();
            }

            return requirement;
        }

        // PUT: api/Requirements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequirement(Guid id, Requirement requirement)
        {
            if (id != requirement.Id)
            {
                return BadRequest();
            }

            _context.Entry(requirement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequirementExists(id))
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

        // POST: api/Requirements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Requirement>> PostRequirement(Requirement requirement)
        {
          if (_context.Requirements == null)
          {
              return Problem("Entity set 'AppDbContext.Requirements'  is null.");
          }
            _context.Requirements.Add(requirement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequirement", new { id = requirement.Id }, requirement);
        }

        // DELETE: api/Requirements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequirement(Guid id)
        {
            if (_context.Requirements == null)
            {
                return NotFound();
            }
            var requirement = await _context.Requirements.FindAsync(id);
            if (requirement == null)
            {
                return NotFound();
            }

            _context.Requirements.Remove(requirement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequirementExists(Guid id)
        {
            return (_context.Requirements?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
