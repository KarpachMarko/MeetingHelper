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
    public class EventNavigationController : Controller
    {
        private readonly AppDbContext _context;

        public EventNavigationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/EventNavigation
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EventNavigations.Include(e => e.NextEvent).Include(e => e.PreviousEvent);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/EventNavigation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.EventNavigations == null)
            {
                return NotFound();
            }

            var eventNavigation = await _context.EventNavigations
                .Include(e => e.NextEvent)
                .Include(e => e.PreviousEvent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventNavigation == null)
            {
                return NotFound();
            }

            return View(eventNavigation);
        }

        // GET: Admin/EventNavigation/Create
        public IActionResult Create()
        {
            ViewData["NextEventId"] = new SelectList(_context.Events, "Id", "Description");
            ViewData["PreviousEventId"] = new SelectList(_context.Events, "Id", "Description");
            return View();
        }

        // POST: Admin/EventNavigation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("PreviousEventId,NextEventId,Id")] EventNavigation eventNavigation)
        {
            if (ModelState.IsValid)
            {
                eventNavigation.Id = Guid.NewGuid();
                _context.Add(eventNavigation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["NextEventId"] = new SelectList(_context.Events, "Id", "Description", eventNavigation.NextEventId);
            ViewData["PreviousEventId"] =
                new SelectList(_context.Events, "Id", "Description", eventNavigation.PreviousEventId);
            return View(eventNavigation);
        }

        // GET: Admin/EventNavigation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.EventNavigations == null)
            {
                return NotFound();
            }

            var eventNavigation = await _context.EventNavigations.FindAsync(id);
            if (eventNavigation == null)
            {
                return NotFound();
            }

            ViewData["NextEventId"] = new SelectList(_context.Events, "Id", "Description", eventNavigation.NextEventId);
            ViewData["PreviousEventId"] =
                new SelectList(_context.Events, "Id", "Description", eventNavigation.PreviousEventId);
            return View(eventNavigation);
        }

        // POST: Admin/EventNavigation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("PreviousEventId,NextEventId,Id")] EventNavigation eventNavigation)
        {
            if (id != eventNavigation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventNavigation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventNavigationExists(eventNavigation.Id))
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

            ViewData["NextEventId"] = new SelectList(_context.Events, "Id", "Description", eventNavigation.NextEventId);
            ViewData["PreviousEventId"] =
                new SelectList(_context.Events, "Id", "Description", eventNavigation.PreviousEventId);
            return View(eventNavigation);
        }

        // GET: Admin/EventNavigation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.EventNavigations == null)
            {
                return NotFound();
            }

            var eventNavigation = await _context.EventNavigations
                .Include(e => e.NextEvent)
                .Include(e => e.PreviousEvent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventNavigation == null)
            {
                return NotFound();
            }

            return View(eventNavigation);
        }

        // POST: Admin/EventNavigation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.EventNavigations == null)
            {
                return Problem("Entity set 'AppDbContext.EventNavigations'  is null.");
            }

            var eventNavigation = await _context.EventNavigations.FindAsync(id);
            if (eventNavigation != null)
            {
                _context.EventNavigations.Remove(eventNavigation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventNavigationExists(Guid id)
        {
            return (_context.EventNavigations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}