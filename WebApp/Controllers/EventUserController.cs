using App.BLL.DTO;
using App.BLL.DTO.Identity;
using App.Contracts.BLL;
using Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

public class EventUserController : Controller
{
    private readonly IAppBll _bll;

    public EventUserController(IAppBll bll)
    {
        _bll = bll;
    }

    // GET: Admin/EventUser
    public async Task<IActionResult> Index()
    {
        var eventUsers = await _bll.EventUsers.GetAllAsync(User.GetUserId());
        return View(eventUsers);
    }

    // GET: Admin/EventUser/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var eventUser = await _bll.EventUsers.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (eventUser == null)
        {
            return NotFound();
        }

        return View(eventUser);
    }

    // GET: Admin/EventUser/Create
    public async Task<IActionResult> Create()
    {
        ViewData["EventId"] = new SelectList(await _bll.Events.GetAllAsync(User.GetUserId()), nameof(Event.Id), nameof(Event.Title));
        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName));
        return View();
    }

    // POST: Admin/EventUser/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EventUser eventUser)
    {
        if (ModelState.IsValid)
        {
            eventUser.Id = Guid.NewGuid();
            eventUser.UserId = User.GetUserId();
            _bll.EventUsers.Add(eventUser);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["EventId"] = new SelectList(await _bll.Events.GetAllAsync(User.GetUserId()), nameof(Event.Id), nameof(Event.Title), eventUser.EventId);
        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName), eventUser.UserId);
        return View(eventUser);
    }

    // GET: Admin/EventUser/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var eventUser = await _bll.EventUsers.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (eventUser == null)
        {
            return NotFound();
        }

        ViewData["EventId"] = new SelectList(await _bll.Events.GetAllAsync(User.GetUserId()), nameof(Event.Id), nameof(Event.Title), eventUser.EventId);
        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName), eventUser.UserId);
        return View(eventUser);
    }

    // POST: Admin/EventUser/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, EventUser eventUser)
    {
        if (id != eventUser.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _bll.EventUsers.UpdateAsync(eventUser, User.GetUserId());
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EventUserExists(eventUser.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["EventId"] = new SelectList(await _bll.Events.GetAllAsync(User.GetUserId()), nameof(Event.Id), nameof(Event.Title), eventUser.EventId);
        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName), eventUser.UserId);
        return View(eventUser);
    }

    // GET: Admin/EventUser/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var eventUser = await _bll.EventUsers.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (eventUser == null)
        {
            return NotFound();
        }

        return View(eventUser);
    }

    // POST: Admin/EventUser/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.EventUsers.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> EventUserExists(Guid id)
    {
        return await _bll.EventUsers.ExistsAsync(id);
    }
}