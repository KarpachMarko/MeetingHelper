using App.BLL.DTO;
using App.Contracts.BLL;
using Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

public class RequirementController : Controller
{
    private readonly IAppBll _bll;

    public RequirementController(IAppBll bll)
    {
        _bll = bll;
    }

    // GET: Admin/Requirement
    public async Task<IActionResult> Index()
    {
        var requirements = await _bll.Requirements.GetAllAsync(User.GetUserId());
        return View(requirements);
    }

    // GET: Admin/Requirement/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var requirement = await _bll.Requirements.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (requirement == null)
        {
            return NotFound();
        }

        return View(requirement);
    }

    // GET: Admin/Requirement/Create
    public async Task<IActionResult> Create()
    {
        ViewData["EventId"] = new SelectList(await _bll.Events.GetAllAsync(User.GetUserId()), nameof(Event.Id), nameof(Event.Title));
        return View();
    }

    // POST: Admin/Requirement/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Requirement requirement)
    {
        if (ModelState.IsValid)
        {
            requirement.Id = Guid.NewGuid();
            _bll.Requirements.Add(requirement);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["EventId"] = new SelectList(await _bll.Events.GetAllAsync(User.GetUserId()), nameof(Event.Id), nameof(Event.Title), requirement.EventId);
        return View(requirement);
    }

    // GET: Admin/Requirement/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var requirement = await _bll.Requirements.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (requirement == null)
        {
            return NotFound();
        }

        ViewData["EventId"] = new SelectList(await _bll.Events.GetAllAsync(User.GetUserId()), nameof(Event.Id), nameof(Event.Title), requirement.EventId);
        return View(requirement);
    }

    // POST: Admin/Requirement/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, Requirement requirement)
    {
        if (id != requirement.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _bll.Requirements.UpdateAsync(requirement);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RequirementExists(requirement.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["EventId"] = new SelectList(await _bll.Events.GetAllAsync(User.GetUserId()), nameof(Event.Id), nameof(Event.Title), requirement.EventId);
        return View(requirement);
    }

    // GET: Admin/Requirement/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var requirement = await _bll.Requirements.FirstOrDefaultAsync(id.Value, User.GetUserId());
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
        await _bll.Requirements.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> RequirementExists(Guid id)
    {
        return await _bll.Requirements.ExistsAsync(id);
    }
}