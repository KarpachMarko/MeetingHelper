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
public class EventsController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly IMapper<Event, App.BLL.DTO.Event> _mapper;

    public EventsController(IAppBll bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = new EventMapper(mapper);
    }
    
    [HttpGet("meeting/{id}")]
    public async Task<ActionResult<IEnumerable<Event>>> GetMeetingEvents(Guid id)
    {
        var events = await _bll.Events.GetMeetingEvents(id, User.GetUserId());
        return Ok(_mapper.Map(events));
    }
    
    [HttpGet("meeting/{id}/first")]
    public async Task<ActionResult<IEnumerable<Event>>> GetFirstMeetingEvents(Guid id)
    {
        var events = await _bll.Events.GetFirstMeetingEvents(id, User.GetUserId(), _bll.EventNavigations);
        return Ok(_mapper.Map(events));
    }
    
    [HttpGet("{id}/next")]
    public async Task<ActionResult<IEnumerable<Event>>> GetNextEvents(Guid id)
    {
        var events = await _bll.Events.GetNextEvents(id, User.GetUserId(), _bll.EventNavigations);
        return Ok(_mapper.Map(events));
    }
    
    [HttpGet("{id}/previous")]
    public async Task<ActionResult<IEnumerable<Event>>> GetPreviousEvents(Guid id)
    {
        var events = await _bll.Events.GetPreviousEvents(id, User.GetUserId(), _bll.EventNavigations);
        return Ok(_mapper.Map(events));
    }

    // GET: api/Events
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
    {
        var events = await _bll.Events.GetAllAsync(User.GetUserId());
        return Ok(_mapper.Map(events));
    }

    // GET: api/Events/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetEvent(Guid id)
    {
        var getEvent = await _bll.Events.FirstOrDefaultAsync(id, User.GetUserId());

        if (getEvent == null)
        {
            return NotFound();
        }

        return _mapper.Map(getEvent)!;
    }

    // PUT: api/Events/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEvent(Guid id, Event putEvent)
    {
        if (id != putEvent.Id)
        {
            return BadRequest();
        }

        await _bll.Events.UpdateAsync(_mapper.Map(putEvent)!);

        try
        {
            await _bll.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await EventExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/Events
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Event>> PostEvent(Event postEvent)
    {
        postEvent.Id = Guid.NewGuid();
        _bll.Events.Add(_mapper.Map(postEvent)!);
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetEvent", new { id = postEvent.Id }, postEvent);
    }

    // DELETE: api/Events/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        await _bll.Events.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    private Task<bool> EventExists(Guid id)
    {
        return _bll.Events.ExistsAsync(id);
    }
}