using App.BLL.DTO;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

public class AnswerOptionController : Controller
{
    private readonly IAppBll _bll;

    public AnswerOptionController(IAppBll bll)
    {
        _bll = bll;
    }

    // GET: Admin/AnswerOption
    public async Task<IActionResult> Index()
    {
        var answerOptions = await _bll.AnswerOptions.GetAllAsync();
        return View(answerOptions);
    }

    // GET: Admin/AnswerOption/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var answerOption = await _bll.AnswerOptions.FirstOrDefaultAsync(id.Value);
        if (answerOption == null)
        {
            return NotFound();
        }

        return View(answerOption);
    }

    // GET: Admin/AnswerOption/Create
    public async Task<IActionResult> Create()
    {
        ViewData["QuestionnaireId"] = new SelectList(await _bll.Questionnaires.GetAllAsync(), nameof(Questionnaire.Id), nameof(Questionnaire.Question));
        return View();
    }

    // POST: Admin/AnswerOption/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AnswerOption answerOption)
    {
        if (ModelState.IsValid)
        {
            answerOption.Id = Guid.NewGuid();
                
            _bll.AnswerOptions.Add(answerOption);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["QuestionnaireId"] =
            new SelectList(await _bll.Questionnaires.GetAllAsync(), nameof(Questionnaire.Id), nameof(Questionnaire.Question), answerOption.QuestionnaireId);
        return View(answerOption);
    }

    // GET: Admin/AnswerOption/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var answerOption = await _bll.AnswerOptions.FirstOrDefaultAsync(id.Value);
        if (answerOption == null)
        {
            return NotFound();
        }

        ViewData["QuestionnaireId"] =
            new SelectList(await _bll.Questionnaires.GetAllAsync(), nameof(Questionnaire.Id), nameof(Questionnaire.Question), answerOption.QuestionnaireId);
        return View(answerOption);
    }

    // POST: Admin/AnswerOption/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, AnswerOption answerOption)
    {
        if (id != answerOption.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _bll.AnswerOptions.UpdateAsync(answerOption);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AnswerOptionExists(answerOption.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["QuestionnaireId"] =
            new SelectList(await _bll.Questionnaires.GetAllAsync(), nameof(Questionnaire.Id), nameof(Questionnaire.Question), answerOption.QuestionnaireId);
        return View(answerOption);
    }

    // GET: Admin/AnswerOption/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var answerOption = await _bll.AnswerOptions.FirstOrDefaultAsync(id.Value);
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
        await _bll.AnswerOptions.RemoveAsync(id);
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> AnswerOptionExists(Guid id)
    {
        return await _bll.AnswerOptions.ExistsAsync(id);
    }
}