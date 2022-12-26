using App.Contracts.BLL;
using App.BLL.DTO;
using App.BLL.DTO.Identity;
using Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

public class MeetingInviteController : Controller
{
    private readonly IAppBll _bll;

    public MeetingInviteController(IAppBll bll)
    {
        _bll = bll;
    }

    // GET: MeetingInvite
    public async Task<IActionResult> Index()
    {
        var meetingInvites = await _bll.MeetingInvites.GetAllAsync(User.GetUserId());
        return View(meetingInvites);
    }

    // GET: MeetingInvite/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var meetingInvite = await _bll.MeetingInvites.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (meetingInvite == null)
        {
            return NotFound();
        }

        return View(meetingInvite);
    }

    // GET: MeetingInvite/Create
    public async Task<IActionResult> Create()
    {
        ViewData["MeetingId"] = new SelectList(await _bll.Meetings.GetAllAsync(User.GetUserId()), nameof(Meeting.Id), nameof(Meeting.Title));
        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName));
        return View();
    }

    // POST: MeetingInvite/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MeetingInvite meetingInvite)
    {
        if (ModelState.IsValid)
        {
            meetingInvite.Id = Guid.NewGuid();
            meetingInvite.UserId = User.GetUserId();
            _bll.MeetingInvites.Add(meetingInvite);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["MeetingId"] = new SelectList(await _bll.Meetings.GetAllAsync(User.GetUserId()), nameof(Meeting.Id), nameof(Meeting.Title));
        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName));
        return View(meetingInvite);
    }

    // GET: MeetingInvite/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var meetingInvite = await _bll.MeetingInvites.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (meetingInvite == null)
        {
            return NotFound();
        }
        ViewData["MeetingId"] = new SelectList(await _bll.Meetings.GetAllAsync(User.GetUserId()), nameof(Meeting.Id), nameof(Meeting.Title));
        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName));
        return View(meetingInvite);
    }

    // POST: MeetingInvite/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Status,MeetingId,UserId,Id")] MeetingInvite meetingInvite)
    {
        if (id != meetingInvite.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _bll.MeetingInvites.UpdateAsync(meetingInvite, User.GetUserId());
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MeetingInviteExists(meetingInvite.Id))
                {
                    return NotFound();
                }

                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["MeetingId"] = new SelectList(await _bll.Meetings.GetAllAsync(User.GetUserId()), nameof(Meeting.Id), nameof(Meeting.Title));
        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName));
        return View(meetingInvite);
    }

    // GET: MeetingInvite/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var meetingInvite = await _bll.MeetingInvites.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (meetingInvite == null)
        {
            return NotFound();
        }

        return View(meetingInvite);
    }

    // POST: MeetingInvite/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.MeetingInvites.RemoveAsync(id, User.GetUserId());
            
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> MeetingInviteExists(Guid id)
    {
        return await _bll.MeetingInvites.ExistsAsync(id);
    }
}