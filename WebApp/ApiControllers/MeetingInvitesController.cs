using App.Contracts.BLL;
using App.Public.DTO.Mappers;
using App.Public.DTO.v1;
using AutoMapper;
using Base.Contracts;
using Base.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "TelegramAuth")]
    public class MeetingInvitesController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly IMapper<MeetingInvite, App.BLL.DTO.MeetingInvite> _mapper;

        public MeetingInvitesController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new MeetingInviteMapper(mapper);
        }

        [HttpGet("unanswered")]
        public async Task<ActionResult<IEnumerable<MeetingInvite>>> GetUnansweredInvites()
        {
            return Ok(_mapper.Map(await _bll.MeetingInvites.GetUnansweredInvites(User.GetUserId())));
        }

        [HttpGet("meeting/{meetingId}/accept")]
        public async Task<ActionResult> AcceptInvite(Guid meetingId)
        {
            var success = await _bll.MeetingInvites.Accept(meetingId, User.GetUserId(), _bll.MeetingUsers,
                _bll.Events, _bll.EventUsers);
            if (!success)
            {
                return NotFound("You have no unanswered invite to this meeting");
            }

            await _bll.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("meeting/{meetingId}/reject")]
        public async Task<ActionResult> RejectInvite(Guid meetingId)
        {
            var success = await _bll.MeetingInvites.Reject(meetingId, User.GetUserId());
            if (!success)
            {
                return NotFound("You have no unanswered invite to this meeting");
            }

            await _bll.SaveChangesAsync();
            return Ok();
        }

        // GET: api/MeetingInvite
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeetingInvite>>> GetMeetingInvites()
        {
            var meetingInvites = await _bll.MeetingInvites.GetAllAsync(User.GetUserId());
            return Ok(_mapper.Map(await _bll.MeetingInvites.GetAllAsync(User.GetUserId())));
        }

        // GET: api/MeetingInvite/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MeetingInvite>> GetMeetingInvite(Guid id)
        {
            var meetingInvite = await _bll.MeetingInvites.FirstOrDefaultAsync(id, User.GetUserId());

            if (meetingInvite == null)
            {
                return NotFound();
            }

            return _mapper.Map(meetingInvite)!;
        }

        // PUT: api/MeetingInvite/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeetingInvite(Guid id, MeetingInvite meetingInvite)
        {
            if (id != meetingInvite.Id)
            {
                return BadRequest();
            }

            await _bll.MeetingInvites.UpdateAsync(_mapper.Map(meetingInvite)!, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MeetingInviteExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/MeetingInvite
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MeetingInvite>> PostMeetingInvite(MeetingInvite meetingInvite)
        {
            var meeting = await _bll.Meetings.FirstOrDefaultAsync(meetingInvite.MeetingId, User.GetUserId());
            if (meeting == null)
            {
                return Unauthorized("You are not allowed to invite to this meeting");
            }

            meetingInvite.Id = Guid.NewGuid();
            var createdInvite = _mapper.Map(_bll.MeetingInvites.Add(_mapper.Map(meetingInvite)!));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetMeetingInvite", new { id = meetingInvite.Id }, createdInvite);
        }

        // DELETE: api/MeetingInvite/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeetingInvite(Guid id)
        {
            var meetingInvite = await _bll.MeetingInvites.FirstOrDefaultAsync(id, User.GetUserId());
            if (meetingInvite == null)
            {
                return NotFound();
            }

            _bll.MeetingInvites.Remove(meetingInvite);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> MeetingInviteExists(Guid id)
        {
            return await _bll.MeetingInvites.ExistsAsync(id);
        }
    }
}