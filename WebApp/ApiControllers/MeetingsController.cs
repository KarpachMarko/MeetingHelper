using App.Contracts.BLL;
using App.Public.DTO.Mappers;
using App.Public.DTO.v1;
using AutoMapper;
using Base.Contracts;
using Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers;

[Route("api/[controller]")]
[ApiController]
public class MeetingsController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly IMapper<Meeting, App.BLL.DTO.Meeting> _mapper;

    public MeetingsController(IAppBll bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = new MeetingMapper(mapper);
    }

    // GET: api/Meetings
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Meeting>>> GetMeetings()
    {
        var meetings = await _bll.Meetings.GetAllAsync(User.GetUserId());
        return Ok(_mapper.Map(meetings));
    }

    // GET: api/Meetings/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Meeting>> GetMeeting(Guid id)
    {
        var meeting = await _bll.Meetings.FirstOrDefaultAsync(id, User.GetUserId());

        if (meeting == null)
        {
            return NotFound();
        }

        return _mapper.Map(meeting)!;
    }

    // PUT: api/Meetings/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMeeting(Guid id, Meeting meeting)
    {
        if (id != meeting.Id)
        {
            return BadRequest();
        }

        await _bll.Meetings.UpdateAsync(_mapper.Map(meeting)!);

        try
        {
            await _bll.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await MeetingExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/Meetings
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Meeting>> PostMeeting(Meeting meeting)
    {
        meeting.Id = Guid.NewGuid();
        _bll.Meetings.Add(_mapper.Map(meeting)!);
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetMeeting", new { id = meeting.Id }, meeting);
    }

    // DELETE: api/Meetings/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMeeting(Guid id)
    {
        await _bll.Meetings.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    private Task<bool> MeetingExists(Guid id)
    {
        return _bll.Meetings.ExistsAsync(id);
    }
}