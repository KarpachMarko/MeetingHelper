using App.BLL.DTO;
using App.BLL.DTO.Identity;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using App.Domain.Enums;
using Base.BLL;
using Base.Contracts;
using Microsoft.IdentityModel.Tokens;

namespace App.BLL.Services;

public class MeetingInviteService : BaseEntityUserService<MeetingInvite, DAL.DTO.MeetingInvite, AppUser,
        DAL.DTO.Identity.AppUser, IMeetingInviteRepository>,
    IMeetingInviteService
{
    public MeetingInviteService(IMeetingInviteRepository repository,
        IMapper<MeetingInvite, DAL.DTO.MeetingInvite> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<MeetingInvite>> GetUnansweredInvites(Guid userId)
    {
        return Mapper.Map(await Repository.GetUnansweredInvites(userId));
    }

    public async Task<bool> Accept(Guid meetingId, Guid userId,
        IMeetingUserService meetingUserService,
        IEventService eventService,
        IEventUserService eventUserService)
    {
        var meetingInvite =
            (await GetUnansweredInvites(userId)).FirstOrDefault(invite => invite.MeetingId.Equals(meetingId));
        if (meetingInvite == null)
        {
            return false;
        }

        meetingInvite.Status = EInviteStatus.Accepted;
        await UpdateAsync(meetingInvite, userId);

        meetingUserService.Add(new MeetingUser
        {
            Id = Guid.NewGuid(),
            MeetingId = meetingId,
            UserId = userId,
            Role = EMeetingRole.Guest
        });

        var meetingEvents = await eventService.GetMeetingEvents(meetingId, userId);
        foreach (var meetingEvent in meetingEvents)
        {
            eventUserService.Add(new EventUser
            {
                Id = Guid.NewGuid(),
                EventId = meetingEvent.Id,
                UserId = userId,
                Status = EEventStatus.Going
            });
        }

        return true;
    }

    public async Task<bool> Reject(Guid meetingId, Guid userId)
    {
        var meetingInvite =
            (await GetUnansweredInvites(userId)).FirstOrDefault(invite => invite.MeetingId.Equals(meetingId));
        if (meetingInvite == null)
        {
            return false;
        }

        meetingInvite.Status = EInviteStatus.Rejected;
        await UpdateAsync(meetingInvite, userId);

        return true;
    }
}