using App.BLL.DTO;
using App.Contracts.BLL;
using Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

public class EventNavigationController : Controller
{
    private readonly IAppBll _bll;

    public EventNavigationController(IAppBll bll)
    {
        _bll = bll;
    }

    // GET: Admin/EventNavigation
    public async Task<IActionResult> Index()
    {
        var eventNavigations = await _bll.EventNavigations.GetAllAsync(User.GetUserId());
        return View(eventNavigations);
    }

    // GET: Admin/EventNavigation/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var eventNavigation = await _bll.EventNavigations.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (eventNavigation == null)
        {
            return NotFound();
        }

        return View(eventNavigation);
    }

    // GET: Admin/EventNavigation/Create
    public async Task<IActionResult> Create()
    {
        ViewData["NextEventId"] = new SelectList(await _bll.Events.GetAllAsync(User.GetUserId()), nameof(Event.Id), nameof(Event.Title));
        ViewData["PreviousEventId"] = new SelectList(await _bll.Events.GetAllAsync(User.GetUserId()), nameof(Event.Id), nameof(Event.Title));
        return View();
    }

    // POST: Admin/EventNavigation/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EventNavigation eventNavigation)
    {
        if (ModelState.IsValid)
        {
            eventNavigation.Id = Guid.NewGuid();
            _bll.EventNavigations.Add(eventNavigation);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["NextEventId"] = new SelectList(await _bll.Events.GetAllAsync(User.GetUserId()), nameof(Event.Id), nameof(Event.Title), eventNavigation.NextEventId);
        ViewData["PreviousEventId"] = new SelectList(await _bll.Events.GetAllAsync(User.GetUserId()), nameof(Event.Id), nameof(Event.Title), eventNavigation.PreviousEventId);
        return View(eventNavigation);
    }

    // GET: Admin/EventNavigation/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var eventNavigation = await _bll.EventNavigations.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (eventNavigation == null)
        {
            return NotFound();
        }

        ViewData["NextEventId"] = new SelectList(await _bll.Events.GetAllAsync(User.GetUserId()), nameof(Event.Id), nameof(Event.Title), eventNavigation.NextEventId);
        ViewData["PreviousEventId"] = new SelectList(await _bll.Events.GetAllAsync(User.GetUserId()), nameof(Event.Id), nameof(Event.Title), eventNavigation.PreviousEventId);
        return View(eventNavigation);
    }

    // POST: Admin/EventNavigation/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, EventNavigation eventNavigation)
    {
        if (id != eventNavigation.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _bll.EventNavigations.UpdateAsync(eventNavigation);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EventNavigationExists(eventNavigation.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["NextEventId"] = new SelectList(await _bll.Events.GetAllAsync(User.GetUserId()), nameof(Event.Id), nameof(Event.Title), eventNavigation.NextEventId);
        ViewData["PreviousEventId"] = new SelectList(await _bll.Events.GetAllAsync(User.GetUserId()), nameof(Event.Id), nameof(Event.Title), eventNavigation.PreviousEventId);
        return View(eventNavigation);
    }

    // GET: Admin/EventNavigation/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var eventNavigation = await _bll.EventNavigations.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (eventNavigation == null)
        {
            return NotFound();
        }

        return View(eventNavigation);
    }

    // POST: Admin/EventNavigation/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.EventNavigations.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> EventNavigationExists(Guid id)
    {
        return await _bll.EventNavigations.ExistsAsync(id);
    }
}