using App.BLL.DTO;
using App.Contracts.BLL;
using Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

public class EventController : Controller
{
    private readonly IAppBll _bll;

    public EventController(IAppBll bll)
    {
        _bll = bll;
    }

    // GET: Admin/Event
    public async Task<IActionResult> Index()
    {
        var meetingEvents = await _bll.Events.GetAllAsync(User.GetUserId());
        return View(meetingEvents);
    }

    // GET: Admin/Event/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dbEvent = await _bll.Events.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (dbEvent == null)
        {
            return NotFound();
        }

        return View(dbEvent);
    }

    // GET: Admin/Event/Create
    public async Task<IActionResult> Create()
    {
        ViewData["MeetingId"] = new SelectList(await _bll.Meetings.GetAllAsync(User.GetUserId()), nameof(Meeting.Id), nameof(Meeting.Title));
        return View();
    }

    // POST: Admin/Event/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Event newEvent)
    {
        if (ModelState.IsValid)
        {
            newEvent.Id = Guid.NewGuid();
            _bll.Events.Add(newEvent);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["MeetingId"] = new SelectList(await _bll.Meetings.GetAllAsync(User.GetUserId()), nameof(Meeting.Id), nameof(Meeting.Title), newEvent.MeetingId);
        return View(newEvent);
    }

    // GET: Admin/Event/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dbEvent = await _bll.Events.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (dbEvent == null)
        {
            return NotFound();
        }

        ViewData["MeetingId"] = new SelectList(await _bll.Meetings.GetAllAsync(User.GetUserId()), nameof(Meeting.Id), nameof(Meeting.Title), dbEvent.MeetingId);
        return View(dbEvent);
    }

    // POST: Admin/Event/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, Event editEvent)
    {
        if (id != editEvent.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _bll.Events.UpdateAsync(editEvent);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EventExists(editEvent.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["MeetingId"] = new SelectList(await _bll.Meetings.GetAllAsync(User.GetUserId()), nameof(Meeting.Id), nameof(Meeting.Title), editEvent.MeetingId);
        return View(editEvent);
    }

    // GET: Admin/Event/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dbEvent = await _bll.Events.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (dbEvent == null)
        {
            return NotFound();
        }

        return View(dbEvent);
    }

    // POST: Admin/Event/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
            
        await _bll.Events.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> EventExists(Guid id)
    {
        return await _bll.Events.ExistsAsync(id);
    }
}