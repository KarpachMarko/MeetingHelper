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
public class MeetingUsersController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly IMapper<MeetingUser, App.BLL.DTO.MeetingUser> _mapper;

    public MeetingUsersController(IAppBll bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = new MeetingUserMapper(mapper);
    }

    // GET: api/MeetingUsers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MeetingUser>>> GetMeetingUsers()
    {
        var meetingUsers = await _bll.MeetingUsers.GetAllAsync(User.GetUserId());
        return Ok(_mapper.Map(meetingUsers));
    }

    // GET: api/MeetingUsers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MeetingUser>> GetMeetingUser(Guid id)
    {
        var meetingUser = await _bll.MeetingUsers.FirstOrDefaultAsync(id, User.GetUserId());

        if (meetingUser == null)
        {
            return NotFound();
        }

        return _mapper.Map(meetingUser)!;
    }

    // PUT: api/MeetingUsers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMeetingUser(Guid id, MeetingUser meetingUser)
    {
        if (id != meetingUser.Id)
        {
            return BadRequest();
        }

        await _bll.MeetingUsers.UpdateAsync(_mapper.Map(meetingUser)!, User.GetUserId());

        try
        {
            await _bll.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await MeetingUserExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/MeetingUsers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<MeetingUser>> PostMeetingUser(MeetingUser meetingUser)
    {
        meetingUser.Id = Guid.NewGuid();
        meetingUser.UserId = User.GetUserId();
        _bll.MeetingUsers.Add(_mapper.Map(meetingUser)!);
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetMeetingUser", new { id = meetingUser.Id }, meetingUser);
    }

    // DELETE: api/MeetingUsers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMeetingUser(Guid id)
    {
        await _bll.MeetingUsers.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    private Task<bool> MeetingUserExists(Guid id)
    {
        return _bll.MeetingUsers.ExistsAsync(id);
    }
}