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
    public class EventNavigationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventNavigationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/EventNavigations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventNavigation>>> GetEventNavigations()
        {
          if (_context.EventNavigations == null)
          {
              return NotFound();
          }
            return await _context.EventNavigations.ToListAsync();
        }

        // GET: api/EventNavigations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventNavigation>> GetEventNavigation(Guid id)
        {
          if (_context.EventNavigations == null)
          {
              return NotFound();
          }
            var eventNavigation = await _context.EventNavigations.FindAsync(id);

            if (eventNavigation == null)
            {
                return NotFound();
            }

            return eventNavigation;
        }

        // PUT: api/EventNavigations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventNavigation(Guid id, EventNavigation eventNavigation)
        {
            if (id != eventNavigation.Id)
            {
                return BadRequest();
            }

            _context.Entry(eventNavigation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventNavigationExists(id))
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

        // POST: api/EventNavigations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventNavigation>> PostEventNavigation(EventNavigation eventNavigation)
        {
          if (_context.EventNavigations == null)
          {
              return Problem("Entity set 'AppDbContext.EventNavigations'  is null.");
          }
            _context.EventNavigations.Add(eventNavigation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventNavigation", new { id = eventNavigation.Id }, eventNavigation);
        }

        // DELETE: api/EventNavigations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventNavigation(Guid id)
        {
            if (_context.EventNavigations == null)
            {
                return NotFound();
            }
            var eventNavigation = await _context.EventNavigations.FindAsync(id);
            if (eventNavigation == null)
            {
                return NotFound();
            }

            _context.EventNavigations.Remove(eventNavigation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventNavigationExists(Guid id)
        {
            return (_context.EventNavigations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
