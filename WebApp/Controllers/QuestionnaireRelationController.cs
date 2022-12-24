using App.BLL.DTO;
using App.Contracts.BLL;
using Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

public class QuestionnaireRelationController : Controller
{
    private readonly IAppBll _bll;

    public QuestionnaireRelationController(IAppBll bll)
    {
        _bll = bll;
    }

    // GET: Admin/QuestionnaireRelation
    public async Task<IActionResult> Index()
    {
        var questionnaireRelations = await _bll.QuestionnaireRelations.GetAllAsync();
        return View(questionnaireRelations);
    }

    // GET: Admin/QuestionnaireRelation/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var questionnaireRelation = await _bll.QuestionnaireRelations.FirstOrDefaultAsync(id.Value);
        if (questionnaireRelation == null)
        {
            return NotFound();
        }

        return View(questionnaireRelation);
    }

    // GET: Admin/QuestionnaireRelation/Create
    public async Task<IActionResult> Create()
    {
        ViewData["EventId"] = new SelectList(await _bll.Events.GetAllAsync(User.GetUserId()), nameof(Event.Id), nameof(Event.Title));
        ViewData["MeetingId"] = new SelectList(await _bll.Meetings.GetAllAsync(User.GetUserId()), nameof(Meeting.Id), nameof(Meeting.Title));
        ViewData["QuestionnaireId"] = new SelectList(await _bll.Questionnaires.GetAllAsync(), nameof(Questionnaire.Id), nameof(Questionnaire.Question));
        ViewData["RequirementId"] = new SelectList(await _bll.Requirements.GetAllAsync(User.GetUserId()), nameof(Requirement.Id), nameof(Requirement.Title));
        return View();
    }

    // POST: Admin/QuestionnaireRelation/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(QuestionnaireRelation questionnaireRelation)
    {
        if (ModelState.IsValid)
        {
            questionnaireRelation.Id = Guid.NewGuid();
            _bll.QuestionnaireRelations.Add(questionnaireRelation);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["EventId"] = new SelectList(await _bll.Events.GetAllAsync(User.GetUserId()), nameof(Event.Id), nameof(Event.Title), questionnaireRelation.EventId);
        ViewData["MeetingId"] = new SelectList(await _bll.Meetings.GetAllAsync(User.GetUserId()), nameof(Meeting.Id), nameof(Meeting.Title), questionnaireRelation.MeetingId);
        ViewData["QuestionnaireId"] = new SelectList(await _bll.Questionnaires.GetAllAsync(), nameof(Questionnaire.Id), nameof(Questionnaire.Question), questionnaireRelation.QuestionnaireId);
        ViewData["RequirementId"] = new SelectList(await _bll.Requirements.GetAllAsync(User.GetUserId()), nameof(Requirement.Id), nameof(Requirement.Title), questionnaireRelation.RequirementId);
        return View(questionnaireRelation);
    }

    // GET: Admin/QuestionnaireRelation/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var questionnaireRelation = await _bll.QuestionnaireRelations.FirstOrDefaultAsync(id.Value);
        if (questionnaireRelation == null)
        {
            return NotFound();
        }

        ViewData["EventId"] = new SelectList(await _bll.Events.GetAllAsync(User.GetUserId()), nameof(Event.Id), nameof(Event.Title), questionnaireRelation.EventId);
        ViewData["MeetingId"] = new SelectList(await _bll.Meetings.GetAllAsync(User.GetUserId()), nameof(Meeting.Id), nameof(Meeting.Title), questionnaireRelation.MeetingId);
        ViewData["QuestionnaireId"] = new SelectList(await _bll.Questionnaires.GetAllAsync(), nameof(Questionnaire.Id), nameof(Questionnaire.Question), questionnaireRelation.QuestionnaireId);
        ViewData["RequirementId"] = new SelectList(await _bll.Requirements.GetAllAsync(User.GetUserId()), nameof(Requirement.Id), nameof(Requirement.Title), questionnaireRelation.RequirementId);
        return View(questionnaireRelation);
    }

    // POST: Admin/QuestionnaireRelation/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, QuestionnaireRelation questionnaireRelation)
    {
        if (id != questionnaireRelation.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _bll.QuestionnaireRelations.UpdateAsync(questionnaireRelation);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await QuestionnaireRelationExists(questionnaireRelation.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["EventId"] = new SelectList(await _bll.Events.GetAllAsync(User.GetUserId()), nameof(Event.Id), nameof(Event.Title), questionnaireRelation.EventId);
        ViewData["MeetingId"] = new SelectList(await _bll.Meetings.GetAllAsync(User.GetUserId()), nameof(Meeting.Id), nameof(Meeting.Title), questionnaireRelation.MeetingId);
        ViewData["QuestionnaireId"] = new SelectList(await _bll.Questionnaires.GetAllAsync(), nameof(Questionnaire.Id), nameof(Questionnaire.Question), questionnaireRelation.QuestionnaireId);
        ViewData["RequirementId"] = new SelectList(await _bll.Requirements.GetAllAsync(User.GetUserId()), nameof(Requirement.Id), nameof(Requirement.Title), questionnaireRelation.RequirementId);
        return View(questionnaireRelation);
    }

    // GET: Admin/QuestionnaireRelation/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var questionnaireRelation = await _bll.QuestionnaireRelations.FirstOrDefaultAsync(id.Value);
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
        await _bll.QuestionnaireRelations.RemoveAsync(id);
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> QuestionnaireRelationExists(Guid id)
    {
        return await _bll.QuestionnaireRelations.ExistsAsync(id);
    }
}