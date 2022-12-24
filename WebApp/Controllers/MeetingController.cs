using App.BLL.DTO;
using App.Contracts.BLL;
using Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

public class MeetingController : Controller
{
    private readonly IAppBll _bll;

    public MeetingController(IAppBll bll)
    {
        _bll = bll;
    }

    // GET: Admin/Meeting
    public async Task<IActionResult> Index()
    {
        return View(await _bll.Meetings.GetAllAsync(User.GetUserId()));
    }

    // GET: Admin/Meeting/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var meeting = await _bll.Meetings.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (meeting == null)
        {
            return NotFound();
        }

        return View(meeting);
    }

    // GET: Admin/Meeting/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/Meeting/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Meeting meeting)
    {
        if (ModelState.IsValid)
        {
            meeting.Id = Guid.NewGuid();
            _bll.Meetings.Add(meeting);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(meeting);
    }

    // GET: Admin/Meeting/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var meeting = await _bll.Meetings.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (meeting == null)
        {
            return NotFound();
        }

        return View(meeting);
    }

    // POST: Admin/Meeting/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, Meeting meeting)
    {
        if (id != meeting.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _bll.Meetings.UpdateAsync(meeting);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MeetingExists(meeting.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(meeting);
    }

    // GET: Admin/Meeting/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var meeting = await _bll.Meetings.FirstOrDefaultAsync(id.Value, User.GetUserId());
        if (meeting == null)
        {
            return NotFound();
        }

        return View(meeting);
    }

    // POST: Admin/Meeting/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.Meetings.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> MeetingExists(Guid id)
    {
        return await _bll.Meetings.ExistsAsync(id);
    }
}