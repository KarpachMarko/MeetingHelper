using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class EventUserRepository :
    BaseEntityUserRepository<EventUser, Domain.EventUser, AppUser, Domain.Identity.AppUser, AppDbContext>,
    IEventUserRepository
{
    protected override IQueryable<Domain.EventUser> CreateQuery(Guid userId, bool noTracking = true)
    {
        return base.CreateQuery(userId, noTracking)
            .Include(eventUser => eventUser.Event);
    }

    public EventUserRepository(AppDbContext dbContext, IMapper<EventUser, Domain.EventUser> mapper) : base(dbContext,
        mapper)
    {
    }

    public async Task<IEnumerable<EventUser>> GetEventUsersInEvent(Guid eventId, Guid userId)
    {
        var eventUsers = await CreateQueryUnsafe()
            .Include(eventUser => eventUser.Event)
            .ThenInclude(meetingEvent => meetingEvent!.Meeting)
            .ThenInclude(meeting => meeting!.MeetingUsers)
            .Where(eventUser => eventUser.EventId.Equals(eventId))
            .Where(eventUser => eventUser.Event!.Meeting!.MeetingUsers!.Any(meetingUser => meetingUser.UserId.Equals(userId)))
            .ToListAsync();

        return Mapper.Map(eventUsers);
    }

    public override EventUser Add(EventUser entity)
    {
        var eventUser = CreateQueryUnsafe()
            .FirstOrDefault(eventUser => eventUser.EventId.Equals(entity.EventId) &&
                                         eventUser.UserId.Equals(entity.UserId));
        
        return eventUser == null ? base.Add(entity) : Mapper.Map(eventUser)!;
    }
}