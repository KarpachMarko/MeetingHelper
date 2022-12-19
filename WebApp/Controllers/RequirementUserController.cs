using App.BLL.DTO;
using App.BLL.DTO.Identity;
using App.Contracts.BLL;
using Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

public class RequirementUserController : Controller
{
    private readonly IAppBll _bll;

    public RequirementUserController(IAppBll bll)
    {
        _bll = bll;
    }

    // GET: Admin/RequirementUser
    public async Task<IActionResult> Index()
    {
        var requirementUsers = await _bll.RequirementUsers.GetAllAsync(User.GetUserId());
        return View(requirementUsers);
    }

    // GET: Admin/RequirementUser/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var requirementUser = await _bll.RequirementUsers.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (requirementUser == null)
        {
            return NotFound();
        }

        return View(requirementUser);
    }

    // GET: Admin/RequirementUser/Create
    public async Task<IActionResult> Create()
    {
        
        ViewData["RequirementId"] = new SelectList(await _bll.Requirements.GetAllAsync(User.GetUserId()), nameof(Requirement.Id), nameof(Requirement.Title));
        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName));
        return View();
    }

    // POST: Admin/RequirementUser/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RequirementUser requirementUser)
    {
        if (ModelState.IsValid)
        {
            requirementUser.Id = Guid.NewGuid();
            requirementUser.UserId = User.GetUserId();
            _bll.RequirementUsers.Add(requirementUser);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["RequirementId"] = new SelectList(await _bll.Requirements.GetAllAsync(User.GetUserId()), nameof(Requirement.Id), nameof(Requirement.Title), requirementUser.RequirementId);
        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName), requirementUser.UserId);
        return View(requirementUser);
    }

    // GET: Admin/RequirementUser/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var requirementUser = await _bll.RequirementUsers.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (requirementUser == null)
        {
            return NotFound();
        }

        ViewData["RequirementId"] = new SelectList(await _bll.Requirements.GetAllAsync(User.GetUserId()), nameof(Requirement.Id), nameof(Requirement.Title), requirementUser.RequirementId);
        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName), requirementUser.UserId);
        return View(requirementUser);
    }

    // POST: Admin/RequirementUser/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, RequirementUser requirementUser)
    {
        if (id != requirementUser.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _bll.RequirementUsers.UpdateAsync(requirementUser, User.GetUserId());
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RequirementUserExists(requirementUser.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["RequirementId"] = new SelectList(await _bll.Requirements.GetAllAsync(User.GetUserId()), nameof(Requirement.Id), nameof(Requirement.Title), requirementUser.RequirementId);
        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName), requirementUser.UserId);
        return View(requirementUser);
    }

    // GET: Admin/RequirementUser/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var requirementUser = await _bll.RequirementUsers.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (requirementUser == null)
        {
            return NotFound();
        }

        return View(requirementUser);
    }

    // POST: Admin/RequirementUser/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.RequirementUsers.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> RequirementUserExists(Guid id)
    {
        return await _bll.RequirementUsers.ExistsAsync(id);
    }
}