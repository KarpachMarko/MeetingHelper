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
    public class RequirementOptionController : Controller
    {
        private readonly AppDbContext _context;

        public RequirementOptionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/RequirementOption
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.RequirementOptions.Include(r => r.Requirement);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/RequirementOption/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.RequirementOptions == null)
            {
                return NotFound();
            }

            var requirementOption = await _context.RequirementOptions
                .Include(r => r.Requirement)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requirementOption == null)
            {
                return NotFound();
            }

            return View(requirementOption);
        }

        // GET: Admin/RequirementOption/Create
        public IActionResult Create()
        {
            ViewData["RequirementId"] = new SelectList(_context.Requirements, "Id", "Description");
            return View();
        }

        // POST: Admin/RequirementOption/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Title,Description,Link,Price,RequirementId,Id")] RequirementOption requirementOption)
        {
            if (ModelState.IsValid)
            {
                requirementOption.Id = Guid.NewGuid();
                _context.Add(requirementOption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["RequirementId"] =
                new SelectList(_context.Requirements, "Id", "Description", requirementOption.RequirementId);
            return View(requirementOption);
        }

        // GET: Admin/RequirementOption/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.RequirementOptions == null)
            {
                return NotFound();
            }

            var requirementOption = await _context.RequirementOptions.FindAsync(id);
            if (requirementOption == null)
            {
                return NotFound();
            }

            ViewData["RequirementId"] =
                new SelectList(_context.Requirements, "Id", "Description", requirementOption.RequirementId);
            return View(requirementOption);
        }

        // POST: Admin/RequirementOption/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Title,Description,Link,Price,RequirementId,Id")] RequirementOption requirementOption)
        {
            if (id != requirementOption.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requirementOption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequirementOptionExists(requirementOption.Id))
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

            ViewData["RequirementId"] =
                new SelectList(_context.Requirements, "Id", "Description", requirementOption.RequirementId);
            return View(requirementOption);
        }

        // GET: Admin/RequirementOption/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.RequirementOptions == null)
            {
                return NotFound();
            }

            var requirementOption = await _context.RequirementOptions
                .Include(r => r.Requirement)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requirementOption == null)
            {
                return NotFound();
            }

            return View(requirementOption);
        }

        // POST: Admin/RequirementOption/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.RequirementOptions == null)
            {
                return Problem("Entity set 'AppDbContext.RequirementOptions'  is null.");
            }

            var requirementOption = await _context.RequirementOptions.FindAsync(id);
            if (requirementOption != null)
            {
                _context.RequirementOptions.Remove(requirementOption);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequirementOptionExists(Guid id)
        {
            return (_context.RequirementOptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}