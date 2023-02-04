using App.BLL.DTO;
using App.Contracts.BLL;
using Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

public class RequirementParameterInOptionController : Controller
{
    private readonly IAppBll _bll;

    public RequirementParameterInOptionController(IAppBll bll)
    {
        _bll = bll;
    }

    // GET: RequirementParameterInOption
    public async Task<IActionResult> Index()
    {
        var parameterInOptions = await _bll.RequirementsParameterInOptions.GetAllAsync();
        return View(parameterInOptions);
    }

    // GET: RequirementParameterInOption/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var requirementParameterInOption = await _bll.RequirementsParameterInOptions.FirstOrDefaultAsync(id.Value);
        if (requirementParameterInOption == null)
        {
            return NotFound();
        }

        return View(requirementParameterInOption);
    }

    // GET: RequirementParameterInOption/Create
    public async Task<IActionResult> Create()
    {
        ViewData["RequirementOptionId"] = new SelectList(await _bll.RequirementOptions.GetAllAsync(),
            nameof(RequirementOption.Id), nameof(RequirementOption.Title));
        ViewData["RequirementParameterId"] =
            new SelectList(await _bll.RequirementParameters.GetAllAsync(User.GetUserId()),
                nameof(RequirementParameter.Id), nameof(RequirementParameter.ParameterDesc));
        return View();
    }

    // POST: RequirementParameterInOption/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RequirementParameterInOption requirementParameterInOption)
    {
        if (ModelState.IsValid)
        {
            requirementParameterInOption.Id = Guid.NewGuid();
            _bll.RequirementsParameterInOptions.Add(requirementParameterInOption);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["RequirementOptionId"] = new SelectList(await _bll.RequirementOptions.GetAllAsync(),
            nameof(RequirementOption.Id), nameof(RequirementOption.Title),
            requirementParameterInOption.RequirementOptionId);
        ViewData["RequirementParameterId"] = new SelectList(
            await _bll.RequirementParameters.GetAllAsync(User.GetUserId()), nameof(RequirementParameter.Id),
            nameof(RequirementParameter.ParameterDesc), requirementParameterInOption.RequirementParameterId);
        return View(requirementParameterInOption);
    }

    // GET: RequirementParameterInOption/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var requirementParameterInOption = await _bll.RequirementsParameterInOptions.FirstOrDefaultAsync(id.Value);
        if (requirementParameterInOption == null)
        {
            return NotFound();
        }

        ViewData["RequirementOptionId"] = new SelectList(await _bll.RequirementOptions.GetAllAsync(),
            nameof(RequirementOption.Id), nameof(RequirementOption.Title),
            requirementParameterInOption.RequirementOptionId);
        ViewData["RequirementParameterId"] = new SelectList(
            await _bll.RequirementParameters.GetAllAsync(User.GetUserId()), nameof(RequirementParameter.Id),
            nameof(RequirementParameter.ParameterDesc), requirementParameterInOption.RequirementParameterId);
        return View(requirementParameterInOption);
    }

    // POST: RequirementParameterInOption/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, RequirementParameterInOption requirementParameterInOption)
    {
        if (id != requirementParameterInOption.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _bll.RequirementsParameterInOptions.UpdateAsync(requirementParameterInOption);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RequirementParameterInOptionExists(requirementParameterInOption.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["RequirementOptionId"] = new SelectList(await _bll.RequirementOptions.GetAllAsync(),
            nameof(RequirementOption.Id), nameof(RequirementOption.Title),
            requirementParameterInOption.RequirementOptionId);
        ViewData["RequirementParameterId"] = new SelectList(
            await _bll.RequirementParameters.GetAllAsync(User.GetUserId()), nameof(RequirementParameter.Id),
            nameof(RequirementParameter.ParameterDesc), requirementParameterInOption.RequirementParameterId);
        return View(requirementParameterInOption);
    }

    // GET: RequirementParameterInOption/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var requirementParameterInOption = await _bll.RequirementsParameterInOptions.FirstOrDefaultAsync(id.Value);
        if (requirementParameterInOption == null)
        {
            return NotFound();
        }

        return View(requirementParameterInOption);
    }

    // POST: RequirementParameterInOption/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.RequirementsParameterInOptions.RemoveAsync(id);
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> RequirementParameterInOptionExists(Guid id)
    {
        return await _bll.RequirementsParameterInOptions.ExistsAsync(id);
    }
}