using App.BLL.DTO;
using App.BLL.DTO.Identity;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

public class MoneyTransferController : Controller
{
    private readonly IAppBll _bll;

    public MoneyTransferController(IAppBll bll)
    {
        _bll = bll;
    }

    // GET: Admin/MoneyTransfer
    public async Task<IActionResult> Index()
    {
        var moneyTransfers = await _bll.MoneyTransfers.GetAllAsync();
        return View(moneyTransfers);
    }

    // GET: Admin/MoneyTransfer/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var moneyTransfer = await _bll.MoneyTransfers.FirstOrDefaultAsync(id.Value);
        if (moneyTransfer == null)
        {
            return NotFound();
        }

        return View(moneyTransfer);
    }

    // GET: Admin/MoneyTransfer/Create
    public async Task<IActionResult> Create()
    {
        ViewData["ReceiverId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName));
        ViewData["SenderId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName));
        return View();
    }

    // POST: Admin/MoneyTransfer/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MoneyTransfer moneyTransfer)
    {
        if (ModelState.IsValid)
        {
            moneyTransfer.Id = Guid.NewGuid();
            _bll.MoneyTransfers.Add(moneyTransfer);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["ReceiverId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName), moneyTransfer.ReceiverId);
        ViewData["SenderId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName), moneyTransfer.SenderId);
        return View(moneyTransfer);
    }

    // GET: Admin/MoneyTransfer/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var moneyTransfer = await _bll.MoneyTransfers.FirstOrDefaultAsync(id.Value);
        if (moneyTransfer == null)
        {
            return NotFound();
        }

        ViewData["ReceiverId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName), moneyTransfer.ReceiverId);
        ViewData["SenderId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName), moneyTransfer.SenderId);
        return View(moneyTransfer);
    }

    // POST: Admin/MoneyTransfer/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, MoneyTransfer moneyTransfer)
    {
        if (id != moneyTransfer.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _bll.MoneyTransfers.UpdateAsync(moneyTransfer);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await MoneyTransferExists(moneyTransfer.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["ReceiverId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName), moneyTransfer.ReceiverId);
        ViewData["SenderId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName), moneyTransfer.SenderId);
        return View(moneyTransfer);
    }

    // GET: Admin/MoneyTransfer/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var moneyTransfer = await _bll.MoneyTransfers.FirstOrDefaultAsync(id.Value);
        if (moneyTransfer == null)
        {
            return NotFound();
        }

        return View(moneyTransfer);
    }

    // POST: Admin/MoneyTransfer/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.MoneyTransfers.RemoveAsync(id);
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> MoneyTransferExists(Guid id)
    {
        return await _bll.MoneyTransfers.ExistsAsync(id);
    }
}