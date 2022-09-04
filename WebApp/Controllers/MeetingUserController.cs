using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.Controllers
{
    public class MeetingUserController : Controller
    {
        private readonly AppDbContext _context;

        public MeetingUserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/MeetingUser
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.MeetingUsers.Include(m => m.Meeting).Include(m => m.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/MeetingUser/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.MeetingUsers == null)
            {
                return NotFound();
            }

            var meetingUser = await _context.MeetingUsers
                .Include(m => m.Meeting)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meetingUser == null)
            {
                return NotFound();
            }

            return View(meetingUser);
        }

        // GET: Admin/MeetingUser/Create
        public IActionResult Create()
        {
            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "TelegrammId");
            return View();
        }

        // POST: Admin/MeetingUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Role,MeetingId,UserId,Id")] MeetingUser meetingUser)
        {
            if (ModelState.IsValid)
            {
                meetingUser.Id = Guid.NewGuid();
                _context.Add(meetingUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Description", meetingUser.MeetingId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "TelegrammId", meetingUser.UserId);
            return View(meetingUser);
        }

        // GET: Admin/MeetingUser/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.MeetingUsers == null)
            {
                return NotFound();
            }

            var meetingUser = await _context.MeetingUsers.FindAsync(id);
            if (meetingUser == null)
            {
                return NotFound();
            }

            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Description", meetingUser.MeetingId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "TelegrammId", meetingUser.UserId);
            return View(meetingUser);
        }

        // POST: Admin/MeetingUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Role,MeetingId,UserId,Id")] MeetingUser meetingUser)
        {
            if (id != meetingUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meetingUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingUserExists(meetingUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Description", meetingUser.MeetingId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "TelegrammId", meetingUser.UserId);
            return View(meetingUser);
        }

        // GET: Admin/MeetingUser/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.MeetingUsers == null)
            {
                return NotFound();
            }

            var meetingUser = await _context.MeetingUsers
                .Include(m => m.Meeting)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meetingUser == null)
            {
                return NotFound();
            }

            return View(meetingUser);
        }

        // POST: Admin/MeetingUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.MeetingUsers == null)
            {
                return Problem("Entity set 'AppDbContext.MeetingUsers'  is null.");
            }

            var meetingUser = await _context.MeetingUsers.FindAsync(id);
            if (meetingUser != null)
            {
                _context.MeetingUsers.Remove(meetingUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingUserExists(Guid id)
        {
            return (_context.MeetingUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}