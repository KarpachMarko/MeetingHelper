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
    public class QuestionnaireController : Controller
    {
        private readonly AppDbContext _context;

        public QuestionnaireController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Questionnaire
        public async Task<IActionResult> Index()
        {
            return _context.Questionnaires != null
                ? View(await _context.Questionnaires.ToListAsync())
                : Problem("Entity set 'AppDbContext.Questionnaires'  is null.");
        }

        // GET: Admin/Questionnaire/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Questionnaires == null)
            {
                return NotFound();
            }

            var questionnaire = await _context.Questionnaires
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionnaire == null)
            {
                return NotFound();
            }

            return View(questionnaire);
        }

        // GET: Admin/Questionnaire/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Questionnaire/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Question,QuestionnaireMsgId,Anonymous,SingleAnswer,ActiveScince,Deadline,Id")]
            Questionnaire questionnaire)
        {
            if (ModelState.IsValid)
            {
                questionnaire.Id = Guid.NewGuid();
                _context.Add(questionnaire);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(questionnaire);
        }

        // GET: Admin/Questionnaire/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Questionnaires == null)
            {
                return NotFound();
            }

            var questionnaire = await _context.Questionnaires.FindAsync(id);
            if (questionnaire == null)
            {
                return NotFound();
            }

            return View(questionnaire);
        }

        // POST: Admin/Questionnaire/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Question,QuestionnaireMsgId,Anonymous,SingleAnswer,ActiveScince,Deadline,Id")]
            Questionnaire questionnaire)
        {
            if (id != questionnaire.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionnaire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionnaireExists(questionnaire.Id))
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

            return View(questionnaire);
        }

        // GET: Admin/Questionnaire/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Questionnaires == null)
            {
                return NotFound();
            }

            var questionnaire = await _context.Questionnaires
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionnaire == null)
            {
                return NotFound();
            }

            return View(questionnaire);
        }

        // POST: Admin/Questionnaire/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Questionnaires == null)
            {
                return Problem("Entity set 'AppDbContext.Questionnaires'  is null.");
            }

            var questionnaire = await _context.Questionnaires.FindAsync(id);
            if (questionnaire != null)
            {
                _context.Questionnaires.Remove(questionnaire);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionnaireExists(Guid id)
        {
            return (_context.Questionnaires?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}