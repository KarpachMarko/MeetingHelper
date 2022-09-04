using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _context;

        public EventController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Event
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Events.Include(b => b.Meeting);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Event/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var dbEvent = await _context.Events
                .Include(b => b.Meeting)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dbEvent == null)
            {
                return NotFound();
            }

            return View(dbEvent);
        }

        // GET: Admin/Event/Create
        public IActionResult Create()
        {
            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Description");
            return View();
        }

        // POST: Admin/Event/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind(
                "Title,Description,StartDate,EndDate,DecisionDate,BudgetPerPerson,MinPersonCount,MaxPersonCount,LocationTitle,LocationLink,MeetingId,Id")]
            Event newEvent)
        {
            if (ModelState.IsValid)
            {
                newEvent.Id = Guid.NewGuid();
                _context.Add(newEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Description", newEvent.MeetingId);
            return View(newEvent);
        }

        // GET: Admin/Event/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var dbEvent = await _context.Events.FindAsync(id);
            if (dbEvent == null)
            {
                return NotFound();
            }

            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Description", dbEvent.MeetingId);
            return View(dbEvent);
        }

        // POST: Admin/Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind(
                "Title,Description,StartDate,EndDate,DecisionDate,BudgetPerPerson,MinPersonCount,MaxPersonCount,LocationTitle,LocationLink,MeetingId,Id")]
            Event editEvent)
        {
            if (id != editEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(editEvent.Id))
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

            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Description", editEvent.MeetingId);
            return View(editEvent);
        }

        // GET: Admin/Event/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var dbEvent = await _context.Events
                .Include(b => b.Meeting)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dbEvent == null)
            {
                return NotFound();
            }

            return View(dbEvent);
        }

        // POST: Admin/Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Events == null)
            {
                return Problem("Entity set 'AppDbContext.Events'  is null.");
            }

            var dbEvent = await _context.Events.FindAsync(id);
            if (dbEvent != null)
            {
                _context.Events.Remove(dbEvent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(Guid id)
        {
            return (_context.Events?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}