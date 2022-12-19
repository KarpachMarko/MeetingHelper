using App.BLL.DTO;
using App.BLL.DTO.Identity;
using App.Contracts.BLL;
using Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

public class MeetingUserController : Controller
{
    private readonly IAppBll _bll;

    public MeetingUserController(IAppBll bll)
    {
        _bll = bll;
    }

    // GET: Admin/MeetingUser
    public async Task<IActionResult> Index()
    {
        var meetingUsers = await _bll.MeetingUsers.GetAllAsync(User.GetUserId());
        return View(meetingUsers);
    }

    // GET: Admin/MeetingUser/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var meetingUser = await _bll.MeetingUsers.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (meetingUser == null)
        {
            return NotFound();
        }

        return View(meetingUser);
    }

    // GET: Admin/MeetingUser/Create
    public async Task<IActionResult> Create()
    {
        ViewData["MeetingId"] = new SelectList(await _bll.Meetings.GetAllAsync(User.GetUserId()), nameof(Meeting.Id), nameof(Meeting.Title));
        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName));
        return View();
    }

    // POST: Admin/MeetingUser/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MeetingUser meetingUser)
    {
        if (ModelState.IsValid)
        {
            meetingUser.Id = Guid.NewGuid();
            _bll.MeetingUsers.Add(meetingUser);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["MeetingId"] = new SelectList(await _bll.Meetings.GetAllAsync(User.GetUserId()), nameof(Meeting.Id), nameof(Meeting.Title), meetingUser.MeetingId);
        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName), meetingUser.UserId);
        return View(meetingUser);
    }

    // GET: Admin/MeetingUser/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var meetingUser = await _bll.MeetingUsers.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (meetingUser == null)
        {
            return NotFound();
        }

        ViewData["MeetingId"] = new SelectList(await _bll.Meetings.GetAllAsync(User.GetUserId()), nameof(Meeting.Id), nameof(Meeting.Title), meetingUser.MeetingId);
        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName), meetingUser.UserId);
        return View(meetingUser);
    }

    // POST: Admin/MeetingUser/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, MeetingUser meetingUser)
    {
        if (id != meetingUser.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _bll.MeetingUsers.UpdateAsync(meetingUser, User.GetUserId());
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MeetingUserExists(meetingUser.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["MeetingId"] = new SelectList(await _bll.Meetings.GetAllAsync(User.GetUserId()), nameof(Meeting.Id), nameof(Meeting.Title), meetingUser.MeetingId);
        ViewData["UserId"] = new SelectList(await _bll.Users.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.UserName), meetingUser.UserId);
        return View(meetingUser);
    }

    // GET: Admin/MeetingUser/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var meetingUser = await _bll.MeetingUsers.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (meetingUser == null)
        {
            return NotFound();
        }

        return View(meetingUser);
    }

    // POST: Admin/MeetingUser/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {

        await _bll.MeetingUsers.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> MeetingUserExists(Guid id)
    {
        return await _bll.MeetingUsers.ExistsAsync(id);
    }
}