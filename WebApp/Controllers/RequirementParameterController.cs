using App.BLL.DTO;
using App.Contracts.BLL;
using Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

public class RequirementParameterController : Controller
{
    private readonly IAppBll _bll;

    public RequirementParameterController(IAppBll bll)
    {
        _bll = bll;
    }

    // GET: RequirementParameter
    public async Task<IActionResult> Index()
    {
        var requirementParameters = await _bll.RequirementParameters.GetAllAsync(User.GetUserId());
        return View(requirementParameters);
    }

    // GET: RequirementParameter/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var requirementParameter = await _bll.RequirementParameters.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (requirementParameter == null)
        {
            return NotFound();
        }

        return View(requirementParameter);
    }

    // GET: RequirementParameter/Create
    public async Task<IActionResult> Create()
    {
        ViewData["RequirementId"] = new SelectList(await _bll.Requirements.GetAllAsync(User.GetUserId()),
            nameof(Requirement.Id), nameof(Requirement.Title));
        return View();
    }

    // POST: RequirementParameter/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RequirementParameter requirementParameter)
    {
        if (ModelState.IsValid)
        {
            requirementParameter.Id = Guid.NewGuid();
            _bll.RequirementParameters.Add(requirementParameter);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["RequirementId"] = new SelectList(await _bll.Requirements.GetAllAsync(User.GetUserId()),
            nameof(Requirement.Id), nameof(Requirement.Title), requirementParameter.RequirementId);
        return View(requirementParameter);
    }

    // GET: RequirementParameter/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var requirementParameter = await _bll.RequirementParameters.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (requirementParameter == null)
        {
            return NotFound();
        }

        ViewData["RequirementId"] = new SelectList(await _bll.Requirements.GetAllAsync(User.GetUserId()),
            nameof(Requirement.Id), nameof(Requirement.Title), requirementParameter.RequirementId);
        return View(requirementParameter);
    }

    // POST: RequirementParameter/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, RequirementParameter requirementParameter)
    {
        if (id != requirementParameter.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _bll.RequirementParameters.UpdateAsync(requirementParameter);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RequirementParameterExists(requirementParameter.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["RequirementId"] = new SelectList(await _bll.Requirements.GetAllAsync(User.GetUserId()),
            nameof(Requirement.Id), nameof(Requirement.Title), requirementParameter.RequirementId);
        return View(requirementParameter);
    }

    // GET: RequirementParameter/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var requirementParameter = await _bll.RequirementParameters.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (requirementParameter == null)
        {
            return NotFound();
        }

        return View(requirementParameter);
    }

    // POST: RequirementParameter/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.RequirementParameters.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> RequirementParameterExists(Guid id)
    {
        return await _bll.RequirementParameters.ExistsAsync(id);
    }
}