using App.BLL.DTO;
using App.BLL.DTO.Identity;
using App.Contracts.BLL;
using Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

public class BankAccountController : Controller
{
    private readonly IAppBll _bll;

    public BankAccountController(IAppBll bll)
    {
        _bll = bll;
    }

    // GET: Admin/BankAccount
    public async Task<IActionResult> Index()
    {
        var bankAccounts = await _bll.BankAccounts.GetAllAsync(User.GetUserId());
        return View(bankAccounts);
    }

    // GET: Admin/BankAccount/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bankAccount = await _bll.BankAccounts.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (bankAccount == null)
        {
            return NotFound();
        }

        return View(bankAccount);
    }

    // GET: Admin/BankAccount/Create
    public async Task<IActionResult> Create()
    {
        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName));
        return View();
    }

    // POST: Admin/BankAccount/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BankAccount bankAccount)
    {
        if (ModelState.IsValid)
        {
            bankAccount.Id = Guid.NewGuid();
            bankAccount.UserId = User.GetUserId();
            _bll.BankAccounts.Add(bankAccount);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName), bankAccount.UserId);
        return View(bankAccount);
    }

    // GET: Admin/BankAccount/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bankAccount = await _bll.BankAccounts.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (bankAccount == null)
        {
            return NotFound();
        }

        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName), bankAccount.UserId);
        return View(bankAccount);
    }

    // POST: Admin/BankAccount/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, BankAccount bankAccount)
    {
        if (id != bankAccount.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _bll.BankAccounts.UpdateAsync(bankAccount, User.GetUserId());
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BankAccountExists(bankAccount.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName), bankAccount.UserId);
        return View(bankAccount);
    }

    // GET: Admin/BankAccount/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bankAccount = await _bll.BankAccounts.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (bankAccount == null)
        {
            return NotFound();
        }

        return View(bankAccount);
    }

    // POST: Admin/BankAccount/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
            
        await _bll.BankAccounts.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> BankAccountExists(Guid id)
    {
        return await _bll.BankAccounts.ExistsAsync(id);
    }
}