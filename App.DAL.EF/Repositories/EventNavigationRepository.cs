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

    private static bool IsInMeeting(EventNavigation eventNav, Guid meetingId)
    {
        return (eventNav.PreviousEvent?.MeetingId.Equals(meetingId) ?? false) ||
               (eventNav.NextEvent?.MeetingId.Equals(meetingId) ?? false);
    }

    public async Task<IEnumerable<EventNavigation>> GetMeetingEventNavigations(Guid meetingId, Guid userId)
    {
        var eventNavigations = await CreateQuery().ToListAsync();

        var meetingEventNavigations = Mapper.Map(eventNavigations)
            .Where(navigation => IsInMeeting(navigation, meetingId))
            .Where(navigation => CheckOwnership(navigation, userId));
        return meetingEventNavigations;
    }

    public async Task<IEnumerable<EventNavigation>> GetNextEventNavigations(Guid eventId, Guid userId)
    {
        var eventNavigations = await CreateQuery()
            .Where(navigation => navigation.PreviousEventId.Equals(eventId))
            .ToListAsync();

        return Mapper.Map(eventNavigations).Where(navigation => CheckOwnership(navigation, userId));
    }

    public async Task<IEnumerable<EventNavigation>> GetPreviousEventNavigations(Guid eventId, Guid userId)
    {
        var eventNavigations = await CreateQuery()
            .Where(navigation => navigation.NextEventId.Equals(eventId))
            .ToListAsync();

        return Mapper.Map(eventNavigations).Where(navigation => CheckOwnership(navigation, userId));
    }

    public override EventNavigation Add(EventNavigation entity)
    {
        var eventNavigation = CreateQuery()
            .FirstOrDefault(navigation => navigation.NextEventId.Equals(entity.NextEventId) &&
                                          navigation.PreviousEventId.Equals(entity.PreviousEventId));

        return eventNavigation == null ? base.Add(entity) : Mapper.Map(eventNavigation)!;
    }
}