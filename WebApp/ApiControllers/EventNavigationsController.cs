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
public class EventNavigationsController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly IMapper<EventNavigation, App.BLL.DTO.EventNavigation> _mapper;

    public EventNavigationsController(IAppBll bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = new EventNavigationMapper(mapper);
    }

    [HttpGet("meeting/{id}")]
    public async Task<ActionResult<IEnumerable<EventNavigation>>> GetEventNavigations(Guid id)
    {
        var eventNavigations = await _bll.EventNavigations.GetMeetingEventNavigations(id, User.GetUserId());
        return Ok(_mapper.Map(eventNavigations));
    }
    
    // GET: api/EventNavigations
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventNavigation>>> GetEventNavigations()
    {
        var eventNavigations = await _bll.EventNavigations.GetAllAsync(User.GetUserId());
        return Ok(_mapper.Map(eventNavigations));
    }

    // GET: api/EventNavigations/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EventNavigation>> GetEventNavigation(Guid id)
    {
        var eventNavigation = await _bll.EventNavigations.FirstOrDefaultAsync(id, User.GetUserId());

        if (eventNavigation == null)
        {
            return NotFound();
        }

        return _mapper.Map(eventNavigation)!;
    }

    // PUT: api/EventNavigations/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEventNavigation(Guid id, EventNavigation eventNavigation)
    {
        if (id != eventNavigation.Id)
        {
            return BadRequest();
        }

        await _bll.EventNavigations.UpdateAsync(_mapper.Map(eventNavigation)!);

        try
        {
            await _bll.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await EventNavigationExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/EventNavigations
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<EventNavigation>> PostEventNavigation(EventNavigation eventNavigation)
    {
        eventNavigation.Id = Guid.NewGuid();
        _bll.EventNavigations.Add(_mapper.Map(eventNavigation)!);
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetEventNavigation", new { id = eventNavigation.Id }, eventNavigation);
    }

    // DELETE: api/EventNavigations/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEventNavigation(Guid id)
    {
        var eventNavigation = await _bll.EventNavigations.FirstOrDefaultAsync(id, User.GetUserId());
        if (eventNavigation == null)
        {
            return NotFound();
        }

        _bll.EventNavigations.Remove(eventNavigation);
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    private Task<bool> EventNavigationExists(Guid id)
    {
        return _bll.EventNavigations.ExistsAsync(id);
    }
}