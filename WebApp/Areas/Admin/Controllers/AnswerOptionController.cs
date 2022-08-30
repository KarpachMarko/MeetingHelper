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
    public class AnswerOptionController : Controller
    {
        private readonly AppDbContext _context;

        public AnswerOptionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AnswerOption
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AnswerOptions.Include(a => a.Questionnaire);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/AnswerOption/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.AnswerOptions == null)
            {
                return NotFound();
            }

            var answerOption = await _context.AnswerOptions
                .Include(a => a.Questionnaire)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (answerOption == null)
            {
                return NotFound();
            }

            return View(answerOption);
        }

        // GET: Admin/AnswerOption/Create
        public IActionResult Create()
        {
            ViewData["QuestionnaireId"] = new SelectList(_context.Questionnaires, "Id", "Question");
            return View();
        }

        // POST: Admin/AnswerOption/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Answer,QuestionnaireId,Id")] AnswerOption answerOption)
        {
            if (ModelState.IsValid)
            {
                answerOption.Id = Guid.NewGuid();
                _context.Add(answerOption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionnaireId"] = new SelectList(_context.Questionnaires, "Id", "Question", answerOption.QuestionnaireId);
            return View(answerOption);
        }

        // GET: Admin/AnswerOption/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.AnswerOptions == null)
            {
                return NotFound();
            }

            var answerOption = await _context.AnswerOptions.FindAsync(id);
            if (answerOption == null)
            {
                return NotFound();
            }
            ViewData["QuestionnaireId"] = new SelectList(_context.Questionnaires, "Id", "Question", answerOption.QuestionnaireId);
            return View(answerOption);
        }

        // POST: Admin/AnswerOption/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Answer,QuestionnaireId,Id")] AnswerOption answerOption)
        {
            if (id != answerOption.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answerOption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerOptionExists(answerOption.Id))
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
            ViewData["QuestionnaireId"] = new SelectList(_context.Questionnaires, "Id", "Question", answerOption.QuestionnaireId);
            return View(answerOption);
        }

        // GET: Admin/AnswerOption/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.AnswerOptions == null)
            {
                return NotFound();
            }

            var answerOption = await _context.AnswerOptions
                .Include(a => a.Questionnaire)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (answerOption == null)
            {
                return NotFound();
            }

            return View(answerOption);
        }

        // POST: Admin/AnswerOption/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.AnswerOptions == null)
            {
                return Problem("Entity set 'AppDbContext.AnswerOptions'  is null.");
            }
            var answerOption = await _context.AnswerOptions.FindAsync(id);
            if (answerOption != null)
            {
                _context.AnswerOptions.Remove(answerOption);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerOptionExists(Guid id)
        {
          return (_context.AnswerOptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
