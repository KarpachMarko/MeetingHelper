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
    public class RequirementUserController : Controller
    {
        private readonly AppDbContext _context;

        public RequirementUserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/RequirementUser
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.RequirementUsers.Include(r => r.Requirement).Include(r => r.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/RequirementUser/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.RequirementUsers == null)
            {
                return NotFound();
            }

            var requirementUser = await _context.RequirementUsers
                .Include(r => r.Requirement)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requirementUser == null)
            {
                return NotFound();
            }

            return View(requirementUser);
        }

        // GET: Admin/RequirementUser/Create
        public IActionResult Create()
        {
            ViewData["RequirementId"] = new SelectList(_context.Requirements, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "TelegrammId");
            return View();
        }

        // POST: Admin/RequirementUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Role,Proportion,RequirementId,UserId,Id")] RequirementUser requirementUser)
        {
            if (ModelState.IsValid)
            {
                requirementUser.Id = Guid.NewGuid();
                _context.Add(requirementUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["RequirementId"] =
                new SelectList(_context.Requirements, "Id", "Description", requirementUser.RequirementId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "TelegrammId", requirementUser.UserId);
            return View(requirementUser);
        }

        // GET: Admin/RequirementUser/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.RequirementUsers == null)
            {
                return NotFound();
            }

            var requirementUser = await _context.RequirementUsers.FindAsync(id);
            if (requirementUser == null)
            {
                return NotFound();
            }

            ViewData["RequirementId"] =
                new SelectList(_context.Requirements, "Id", "Description", requirementUser.RequirementId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "TelegrammId", requirementUser.UserId);
            return View(requirementUser);
        }

        // POST: Admin/RequirementUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Role,Proportion,RequirementId,UserId,Id")] RequirementUser requirementUser)
        {
            if (id != requirementUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requirementUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequirementUserExists(requirementUser.Id))
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
                new SelectList(_context.Requirements, "Id", "Description", requirementUser.RequirementId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "TelegrammId", requirementUser.UserId);
            return View(requirementUser);
        }

        // GET: Admin/RequirementUser/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.RequirementUsers == null)
            {
                return NotFound();
            }

            var requirementUser = await _context.RequirementUsers
                .Include(r => r.Requirement)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requirementUser == null)
            {
                return NotFound();
            }

            return View(requirementUser);
        }

        // POST: Admin/RequirementUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.RequirementUsers == null)
            {
                return Problem("Entity set 'AppDbContext.RequirementUsers'  is null.");
            }

            var requirementUser = await _context.RequirementUsers.FindAsync(id);
            if (requirementUser != null)
            {
                _context.RequirementUsers.Remove(requirementUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequirementUserExists(Guid id)
        {
            return (_context.RequirementUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}