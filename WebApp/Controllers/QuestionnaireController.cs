using App.BLL.DTO;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

public class QuestionnaireController : Controller
{
    private readonly IAppBll _bll;

    public QuestionnaireController(IAppBll bll)
    {
        _bll = bll;
    }

    // GET: Admin/Questionnaire
    public async Task<IActionResult> Index()
    {
        return View(await _bll.Questionnaires.GetAllAsync());
    }

    // GET: Admin/Questionnaire/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var questionnaire = await _bll.Questionnaires.FirstOrDefaultAsync(id.Value);
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
    public async Task<IActionResult> Create(Questionnaire questionnaire)
    {
        if (ModelState.IsValid)
        {
            questionnaire.Id = Guid.NewGuid();
            _bll.Questionnaires.Add(questionnaire);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(questionnaire);
    }

    // GET: Admin/Questionnaire/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var questionnaire = await _bll.Questionnaires.FirstOrDefaultAsync(id.Value);
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
    public async Task<IActionResult> Edit(Guid id, Questionnaire questionnaire)
    {
        if (id != questionnaire.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _bll.Questionnaires.UpdateAsync(questionnaire);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await QuestionnaireExists(questionnaire.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(questionnaire);
    }

    // GET: Admin/Questionnaire/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var questionnaire = await _bll.Questionnaires.FirstOrDefaultAsync(id.Value);
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
        await _bll.Questionnaires.RemoveAsync(id);
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> QuestionnaireExists(Guid id)
    {
        return await _bll.Questionnaires.ExistsAsync(id);
    }
}