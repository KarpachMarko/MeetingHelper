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
    public class RequirementUsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RequirementUsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RequirementUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequirementUser>>> GetRequirementUsers()
        {
          if (_context.RequirementUsers == null)
          {
              return NotFound();
          }
            return await _context.RequirementUsers.ToListAsync();
        }

        // GET: api/RequirementUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequirementUser>> GetRequirementUser(Guid id)
        {
          if (_context.RequirementUsers == null)
          {
              return NotFound();
          }
            var requirementUser = await _context.RequirementUsers.FindAsync(id);

            if (requirementUser == null)
            {
                return NotFound();
            }

            return requirementUser;
        }

        // PUT: api/RequirementUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequirementUser(Guid id, RequirementUser requirementUser)
        {
            if (id != requirementUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(requirementUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequirementUserExists(id))
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

        // POST: api/RequirementUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RequirementUser>> PostRequirementUser(RequirementUser requirementUser)
        {
          if (_context.RequirementUsers == null)
          {
              return Problem("Entity set 'AppDbContext.RequirementUsers'  is null.");
          }
            _context.RequirementUsers.Add(requirementUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequirementUser", new { id = requirementUser.Id }, requirementUser);
        }

        // DELETE: api/RequirementUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequirementUser(Guid id)
        {
            if (_context.RequirementUsers == null)
            {
                return NotFound();
            }
            var requirementUser = await _context.RequirementUsers.FindAsync(id);
            if (requirementUser == null)
            {
                return NotFound();
            }

            _context.RequirementUsers.Remove(requirementUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequirementUserExists(Guid id)
        {
            return (_context.RequirementUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
