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
    public class QuestionnaireRelationController : Controller
    {
        private readonly AppDbContext _context;

        public QuestionnaireRelationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/QuestionnaireRelation
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.QuestionnaireRelations.Include(q => q.Event).Include(q => q.Meeting)
                .Include(q => q.Questionnaire).Include(q => q.Requirement);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/QuestionnaireRelation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.QuestionnaireRelations == null)
            {
                return NotFound();
            }

            var questionnaireRelation = await _context.QuestionnaireRelations
                .Include(q => q.Event)
                .Include(q => q.Meeting)
                .Include(q => q.Questionnaire)
                .Include(q => q.Requirement)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionnaireRelation == null)
            {
                return NotFound();
            }

            return View(questionnaireRelation);
        }

        // GET: Admin/QuestionnaireRelation/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description");
            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Description");
            ViewData["QuestionnaireId"] = new SelectList(_context.Questionnaires, "Id", "Question");
            ViewData["RequirementId"] = new SelectList(_context.Requirements, "Id", "Description");
            return View();
        }

        // POST: Admin/QuestionnaireRelation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("QuestionnaireId,MeetingId,EventId,RequirementId,Id")] QuestionnaireRelation questionnaireRelation)
        {
            if (ModelState.IsValid)
            {
                questionnaireRelation.Id = Guid.NewGuid();
                _context.Add(questionnaireRelation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description", questionnaireRelation.EventId);
            ViewData["MeetingId"] =
                new SelectList(_context.Meetings, "Id", "Description", questionnaireRelation.MeetingId);
            ViewData["QuestionnaireId"] = new SelectList(_context.Questionnaires, "Id", "Question",
                questionnaireRelation.QuestionnaireId);
            ViewData["RequirementId"] = new SelectList(_context.Requirements, "Id", "Description",
                questionnaireRelation.RequirementId);
            return View(questionnaireRelation);
        }

        // GET: Admin/QuestionnaireRelation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.QuestionnaireRelations == null)
            {
                return NotFound();
            }

            var questionnaireRelation = await _context.QuestionnaireRelations.FindAsync(id);
            if (questionnaireRelation == null)
            {
                return NotFound();
            }

            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description", questionnaireRelation.EventId);
            ViewData["MeetingId"] =
                new SelectList(_context.Meetings, "Id", "Description", questionnaireRelation.MeetingId);
            ViewData["QuestionnaireId"] = new SelectList(_context.Questionnaires, "Id", "Question",
                questionnaireRelation.QuestionnaireId);
            ViewData["RequirementId"] = new SelectList(_context.Requirements, "Id", "Description",
                questionnaireRelation.RequirementId);
            return View(questionnaireRelation);
        }

        // POST: Admin/QuestionnaireRelation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("QuestionnaireId,MeetingId,EventId,RequirementId,Id")] QuestionnaireRelation questionnaireRelation)
        {
            if (id != questionnaireRelation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionnaireRelation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionnaireRelationExists(questionnaireRelation.Id))
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

            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description", questionnaireRelation.EventId);
            ViewData["MeetingId"] =
                new SelectList(_context.Meetings, "Id", "Description", questionnaireRelation.MeetingId);
            ViewData["QuestionnaireId"] = new SelectList(_context.Questionnaires, "Id", "Question",
                questionnaireRelation.QuestionnaireId);
            ViewData["RequirementId"] = new SelectList(_context.Requirements, "Id", "Description",
                questionnaireRelation.RequirementId);
            return View(questionnaireRelation);
        }

        // GET: Admin/QuestionnaireRelation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.QuestionnaireRelations == null)
            {
                return NotFound();
            }

            var questionnaireRelation = await _context.QuestionnaireRelations
                .Include(q => q.Event)
                .Include(q => q.Meeting)
                .Include(q => q.Questionnaire)
                .Include(q => q.Requirement)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionnaireRelation == null)
            {
                return NotFound();
            }

            return View(questionnaireRelation);
        }

        // POST: Admin/QuestionnaireRelation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.QuestionnaireRelations == null)
            {
                return Problem("Entity set 'AppDbContext.QuestionnaireRelations'  is null.");
            }

            var questionnaireRelation = await _context.QuestionnaireRelations.FindAsync(id);
            if (questionnaireRelation != null)
            {
                _context.QuestionnaireRelations.Remove(questionnaireRelation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionnaireRelationExists(Guid id)
        {
            return (_context.QuestionnaireRelations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}