using App.Contracts.BLL;
using App.Public.DTO.Mappers;
using App.Public.DTO.v1;
using AutoMapper;
using Base.Contracts;
using Base.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "TelegramAuth")]
public class EventUsersController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly IMapper<EventUser, App.BLL.DTO.EventUser> _mapper;

    public EventUsersController(IAppBll bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = new EventUserMapper(mapper);
    }
    
    [HttpGet("event/{id}")]
    public async Task<ActionResult<IEnumerable<MeetingUser>>> GetMeetingUsersInMeeting(Guid id)
    {
        var eventUsers = await _bll.EventUsers.GetEventUsersInEvent(id, User.GetUserId());
        return Ok(_mapper.Map(eventUsers));
    }

    // GET: api/EventUsers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventUser>>> GetEventUsers()
    {
        var userEvents = await _bll.EventUsers.GetAllAsync(User.GetUserId());
        return Ok(_mapper.Map(userEvents));
    }

    // GET: api/EventUsers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EventUser>> GetEventUser(Guid id)
    {
        var eventUser = await _bll.EventUsers.FirstOrDefaultAsync(id, User.GetUserId());

        if (eventUser == null)
        {
            return NotFound();
        }

        return _mapper.Map(eventUser)!;
    }

    // PUT: api/EventUsers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEventUser(Guid id, EventUser eventUser)
    {
        if (id != eventUser.Id)
        {
            return BadRequest();
        }

        await _bll.EventUsers.UpdateAsync(_mapper.Map(eventUser)!, User.GetUserId());

        try
        {
            await _bll.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await EventUserExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/EventUsers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<EventUser>> PostEventUser(EventUser eventUser)
    {
        eventUser.Id = Guid.NewGuid();
        eventUser.UserId = User.GetUserId();
        _bll.EventUsers.Add(_mapper.Map(eventUser)!);
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetEventUser", new { id = eventUser.Id }, eventUser);
    }

    // DELETE: api/EventUsers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEventUser(Guid id)
    {
        await _bll.EventUsers.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    private Task<bool> EventUserExists(Guid id)
    {
        return _bll.EventUsers.ExistsAsync(id);
    }
}