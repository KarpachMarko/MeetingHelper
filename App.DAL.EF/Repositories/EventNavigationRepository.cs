using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class EventNavigationRepository :
    BaseEntityUserDependentRepository<EventNavigation, Domain.EventNavigation, AppDbContext>, IEventNavigationRepository
{
    public EventNavigationRepository(AppDbContext dbContext,
        IMapper<EventNavigation, Domain.EventNavigation> mapper) : base(dbContext, CheckOwnership, mapper)
    {
    }

    protected override IQueryable<Domain.EventNavigation> CreateQuery(bool noTracking = true)
    {
        return base.CreateQuery(noTracking)
            .Include(navigation => navigation.NextEvent)
            .ThenInclude(meetingEvent => meetingEvent!.Meeting)
            .ThenInclude(meeting => meeting!.MeetingUsers)
            .Include(navigation => navigation.PreviousEvent)
            .ThenInclude(meetingEvent => meetingEvent!.Meeting)
            .ThenInclude(meeting => meeting!.MeetingUsers);
    }

    public static bool CheckOwnership(EventNavigation eventNav, Guid userId)
    {
        var meetingUsers = eventNav.PreviousEvent?.Meeting?.MeetingUsers?.ToList() ?? new List<MeetingUser>();
        meetingUsers.AddRange(eventNav.NextEvent?.Meeting?.MeetingUsers?.ToList() ?? new List<MeetingUser>());
        return meetingUsers.Any(meetingUser => meetingUser.UserId.Equals(userId));
    }
}