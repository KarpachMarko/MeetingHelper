using App.BLL.DTO;
using App.BLL.DTO.Identity;
using App.Contracts.BLL;
using Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

public class PaymentController : Controller
{
    private readonly IAppBll _bll;

    public PaymentController(IAppBll bll)
    {
        _bll = bll;
    }

    // GET: Admin/Payment
    public async Task<IActionResult> Index()
    {
        var payments = await _bll.Payments.GetAllAsync(User.GetUserId());
        return View(payments);
    }

    // GET: Admin/Payment/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var payment = await _bll.Payments.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (payment == null)
        {
            return NotFound();
        }

        return View(payment);
    }

    // GET: Admin/Payment/Create
    public async Task<IActionResult> Create()
    {
        ViewData["RequirementId"] = new SelectList(await _bll.Requirements.GetAllAsync(User.GetUserId()), nameof(Requirement.Id), nameof(Requirement.Title));
        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName));
        return View();
    }

    // POST: Admin/Payment/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Payment payment)
    {
        if (ModelState.IsValid)
        {
            payment.Id = Guid.NewGuid();
            payment.UserId = User.GetUserId();
            _bll.Payments.Add(payment);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["RequirementId"] = new SelectList(await _bll.Requirements.GetAllAsync(User.GetUserId()), nameof(Requirement.Id), nameof(Requirement.Title), payment.RequirementId);
        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName), payment.UserId);
        return View(payment);
    }

    // GET: Admin/Payment/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var payment = await _bll.Payments.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (payment == null)
        {
            return NotFound();
        }

        ViewData["RequirementId"] = new SelectList(await _bll.Requirements.GetAllAsync(User.GetUserId()), nameof(Requirement.Id), nameof(Requirement.Title), payment.RequirementId);
        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName), payment.UserId);
        return View(payment);
    }

    // POST: Admin/Payment/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, Payment payment)
    {
        if (id != payment.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _bll.Payments.UpdateAsync(payment, User.GetUserId());
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PaymentExists(payment.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["RequirementId"] = new SelectList(await _bll.Requirements.GetAllAsync(User.GetUserId()), nameof(Requirement.Id), nameof(Requirement.Title), payment.RequirementId);
        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName), payment.UserId);
        return View(payment);
    }

    // GET: Admin/Payment/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var payment = await _bll.Payments.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (payment == null)
        {
            return NotFound();
        }

        return View(payment);
    }

    // POST: Admin/Payment/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.Payments.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> PaymentExists(Guid id)
    {
        return await _bll.Payments.ExistsAsync(id);
    }
}