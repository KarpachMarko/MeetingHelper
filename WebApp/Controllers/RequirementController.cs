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
    public class RequirementController : Controller
    {
        private readonly AppDbContext _context;

        public RequirementController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Requirement
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Requirements.Include(r => r.Event);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Requirement/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Requirements == null)
            {
                return NotFound();
            }

            var requirement = await _context.Requirements
                .Include(r => r.Event)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requirement == null)
            {
                return NotFound();
            }

            return View(requirement);
        }

        // GET: Admin/Requirement/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description");
            return View();
        }

        // POST: Admin/Requirement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Title,Description,BudgetPerPerson,DecisionDate,PaymentDate,EventId,Id")] Requirement requirement)
        {
            if (ModelState.IsValid)
            {
                requirement.Id = Guid.NewGuid();
                _context.Add(requirement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description", requirement.EventId);
            return View(requirement);
        }

        // GET: Admin/Requirement/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Requirements == null)
            {
                return NotFound();
            }

            var requirement = await _context.Requirements.FindAsync(id);
            if (requirement == null)
            {
                return NotFound();
            }

            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description", requirement.EventId);
            return View(requirement);
        }

        // POST: Admin/Requirement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Title,Description,BudgetPerPerson,DecisionDate,PaymentDate,EventId,Id")] Requirement requirement)
        {
            if (id != requirement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requirement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequirementExists(requirement.Id))
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

            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description", requirement.EventId);
            return View(requirement);
        }

        // GET: Admin/Requirement/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Requirements == null)
            {
                return NotFound();
            }

            var requirement = await _context.Requirements
                .Include(r => r.Event)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requirement == null)
            {
                return NotFound();
            }

            return View(requirement);
        }

        // POST: Admin/Requirement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Requirements == null)
            {
                return Problem("Entity set 'AppDbContext.Requirements'  is null.");
            }

            var requirement = await _context.Requirements.FindAsync(id);
            if (requirement != null)
            {
                _context.Requirements.Remove(requirement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequirementExists(Guid id)
        {
            return (_context.Requirements?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}