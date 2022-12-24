using App.BLL.DTO;
using App.Contracts.BLL;
using Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

public class RequirementOptionController : Controller
{
    private readonly IAppBll _bll;

    public RequirementOptionController(IAppBll bll)
    {
        _bll = bll;
    }

    // GET: Admin/RequirementOption
    public async Task<IActionResult> Index()
    {
        var requirementOptions = await _bll.RequirementOptions.GetAllAsync();
        return View(requirementOptions);
    }

    // GET: Admin/RequirementOption/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var requirementOption = await _bll.RequirementOptions.FirstOrDefaultAsync(id.Value);
        if (requirementOption == null)
        {
            return NotFound();
        }

        return View(requirementOption);
    }

    // GET: Admin/RequirementOption/Create
    public async Task<IActionResult> Create()
    {
        ViewData["RequirementId"] = new SelectList(await _bll.Requirements.GetAllAsync(User.GetUserId()), nameof(Requirement.Id), nameof(Requirement.Title));
        return View();
    }

    // POST: Admin/RequirementOption/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RequirementOption requirementOption)
    {
        if (ModelState.IsValid)
        {
            requirementOption.Id = Guid.NewGuid();
            _bll.RequirementOptions.Add(requirementOption);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["RequirementId"] = new SelectList(await _bll.Requirements.GetAllAsync(User.GetUserId()), nameof(Requirement.Id), nameof(Requirement.Title), requirementOption.RequirementId);
        return View(requirementOption);
    }

    // GET: Admin/RequirementOption/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var requirementOption = await _bll.RequirementOptions.FirstOrDefaultAsync(id.Value);
        if (requirementOption == null)
        {
            return NotFound();
        }

        ViewData["RequirementId"] = new SelectList(await _bll.Requirements.GetAllAsync(User.GetUserId()), nameof(Requirement.Id), nameof(Requirement.Title), requirementOption.RequirementId);
        return View(requirementOption);
    }

    // POST: Admin/RequirementOption/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, RequirementOption requirementOption)
    {
        if (id != requirementOption.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _bll.RequirementOptions.UpdateAsync(requirementOption);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RequirementOptionExists(requirementOption.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["RequirementId"] = new SelectList(await _bll.Requirements.GetAllAsync(User.GetUserId()), nameof(Requirement.Id), nameof(Requirement.Title), requirementOption.RequirementId);
        return View(requirementOption);
    }

    // GET: Admin/RequirementOption/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var requirementOption = await _bll.RequirementOptions.FirstOrDefaultAsync(id.Value);
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
        await _bll.RequirementOptions.RemoveAsync(id);
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> RequirementOptionExists(Guid id)
    {
        return await _bll.RequirementOptions.ExistsAsync(id);
    }
}