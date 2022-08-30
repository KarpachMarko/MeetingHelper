using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EventUserController : Controller
    {
        private readonly AppDbContext _context;

        public EventUserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/EventUser
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EventUsers.Include(e => e.Event).Include(e => e.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/EventUser/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.EventUsers == null)
            {
                return NotFound();
            }

            var eventUser = await _context.EventUsers
                .Include(e => e.Event)
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventUser == null)
            {
                return NotFound();
            }

            return View(eventUser);
        }

        // GET: Admin/EventUser/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "TelegrammId");
            return View();
        }

        // POST: Admin/EventUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Status,EventId,UserId,Id")] EventUser eventUser)
        {
            if (ModelState.IsValid)
            {
                eventUser.Id = Guid.NewGuid();
                _context.Add(eventUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description", eventUser.EventId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "TelegrammId", eventUser.UserId);
            return View(eventUser);
        }

        // GET: Admin/EventUser/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.EventUsers == null)
            {
                return NotFound();
            }

            var eventUser = await _context.EventUsers.FindAsync(id);
            if (eventUser == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description", eventUser.EventId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "TelegrammId", eventUser.UserId);
            return View(eventUser);
        }

        // POST: Admin/EventUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Status,EventId,UserId,Id")] EventUser eventUser)
        {
            if (id != eventUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventUserExists(eventUser.Id))
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
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description", eventUser.EventId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "TelegrammId", eventUser.UserId);
            return View(eventUser);
        }

        // GET: Admin/EventUser/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.EventUsers == null)
            {
                return NotFound();
            }

            var eventUser = await _context.EventUsers
                .Include(e => e.Event)
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventUser == null)
            {
                return NotFound();
            }

            return View(eventUser);
        }

        // POST: Admin/EventUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.EventUsers == null)
            {
                return Problem("Entity set 'AppDbContext.EventUsers'  is null.");
            }
            var eventUser = await _context.EventUsers.FindAsync(id);
            if (eventUser != null)
            {
                _context.EventUsers.Remove(eventUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventUserExists(Guid id)
        {
          return (_context.EventUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
