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
    public class MeetingUsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MeetingUsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MeetingUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeetingUser>>> GetMeetingUsers()
        {
          if (_context.MeetingUsers == null)
          {
              return NotFound();
          }
            return await _context.MeetingUsers.ToListAsync();
        }

        // GET: api/MeetingUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MeetingUser>> GetMeetingUser(Guid id)
        {
          if (_context.MeetingUsers == null)
          {
              return NotFound();
          }
            var meetingUser = await _context.MeetingUsers.FindAsync(id);

            if (meetingUser == null)
            {
                return NotFound();
            }

            return meetingUser;
        }

        // PUT: api/MeetingUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeetingUser(Guid id, MeetingUser meetingUser)
        {
            if (id != meetingUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(meetingUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetingUserExists(id))
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

        // POST: api/MeetingUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MeetingUser>> PostMeetingUser(MeetingUser meetingUser)
        {
          if (_context.MeetingUsers == null)
          {
              return Problem("Entity set 'AppDbContext.MeetingUsers'  is null.");
          }
            _context.MeetingUsers.Add(meetingUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeetingUser", new { id = meetingUser.Id }, meetingUser);
        }

        // DELETE: api/MeetingUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeetingUser(Guid id)
        {
            if (_context.MeetingUsers == null)
            {
                return NotFound();
            }
            var meetingUser = await _context.MeetingUsers.FindAsync(id);
            if (meetingUser == null)
            {
                return NotFound();
            }

            _context.MeetingUsers.Remove(meetingUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MeetingUserExists(Guid id)
        {
            return (_context.MeetingUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
