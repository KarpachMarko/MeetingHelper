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
    public class EventUsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventUsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/EventUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventUser>>> GetEventUsers()
        {
          if (_context.EventUsers == null)
          {
              return NotFound();
          }
            return await _context.EventUsers.ToListAsync();
        }

        // GET: api/EventUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventUser>> GetEventUser(Guid id)
        {
          if (_context.EventUsers == null)
          {
              return NotFound();
          }
            var eventUser = await _context.EventUsers.FindAsync(id);

            if (eventUser == null)
            {
                return NotFound();
            }

            return eventUser;
        }

        // PUT: api/EventUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventUser(Guid id, EventUser eventUser)
        {
            if (id != eventUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(eventUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventUserExists(id))
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

        // POST: api/EventUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventUser>> PostEventUser(EventUser eventUser)
        {
          if (_context.EventUsers == null)
          {
              return Problem("Entity set 'AppDbContext.EventUsers'  is null.");
          }
            _context.EventUsers.Add(eventUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventUser", new { id = eventUser.Id }, eventUser);
        }

        // DELETE: api/EventUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventUser(Guid id)
        {
            if (_context.EventUsers == null)
            {
                return NotFound();
            }
            var eventUser = await _context.EventUsers.FindAsync(id);
            if (eventUser == null)
            {
                return NotFound();
            }

            _context.EventUsers.Remove(eventUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventUserExists(Guid id)
        {
            return (_context.EventUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
