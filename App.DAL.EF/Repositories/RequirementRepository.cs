using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class RequirementRepository : BaseEntityUserDependentRepository<Requirement, Domain.Requirement, AppDbContext>,
    IRequirementRepository
{
    public RequirementRepository(AppDbContext dbContext,
        IMapper<Requirement, Domain.Requirement> mapper) : base(dbContext, CheckOwnership, mapper)
    {
    }

    protected override IQueryable<Domain.Requirement> CreateQuery(bool noTracking = true)
    {
        return base.CreateQuery(noTracking)
            .Include(requirement => requirement.Event)
            .ThenInclude(meetingEvent => meetingEvent!.Meeting)
            .ThenInclude(meeting => meeting!.MeetingUsers);
    }

    public static bool CheckOwnership(Requirement requirement, Guid userId)
    {
        return requirement.Event?.Meeting?.MeetingUsers?.Any(meetingUser => meetingUser.UserId.Equals(userId)) ?? false;
    }
    
    public async Task<IEnumerable<Requirement>> GetAllInMeeting(Guid meetingId)
    {
        var requirements = await CreateQuery()
            .Include(requirement => requirement.RequirementUsers)
            .Where(requirement => requirement.Event != null && requirement.Event.MeetingId.Equals(meetingId))
            .ToListAsync();

        return Mapper.Map(requirements);
    }

    public async Task<IEnumerable<Requirement>> GetAllInEvent(Guid eventId, Guid userId)
    {
        var requirements = await CreateQuery()
            .Where(requirement => requirement.EventId.Equals(eventId))
            .ToListAsync();

        return Mapper.Map(requirements).Where(requirement => CheckOwnership(requirement, userId)).ToList();
    }
}