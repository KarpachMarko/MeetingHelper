using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class RequirementUserRepository :
    BaseEntityUserRepository<RequirementUser, Domain.RequirementUser, AppUser, Domain.Identity.AppUser, AppDbContext>,
    IRequirementUserRepository
{
    public RequirementUserRepository(AppDbContext dbContext, IMapper<RequirementUser, Domain.RequirementUser> mapper) :
        base(dbContext, mapper)
    {
    }

    protected override IQueryable<Domain.RequirementUser> CreateQuery(Guid userId, bool noTracking = true)
    {
        return base.CreateQuery(userId, noTracking)
            .Include(requirementUser => requirementUser.Requirement);
    }

    protected override IQueryable<Domain.RequirementUser> CreateQueryUnsafe(bool noTracking = true)
    {
        return base.CreateQueryUnsafe(noTracking)
            .Include(requirementUser => requirementUser.Requirement);
    }

    public override RequirementUser Add(RequirementUser entity)
    {
        var requirementUser = CreateQueryUnsafe()
            .FirstOrDefault(requirementUser => requirementUser.RequirementId.Equals(entity.RequirementId) &&
                                               requirementUser.UserId.Equals(entity.UserId));

        return requirementUser == null ? base.Add(entity) : Mapper.Map(requirementUser)!;
    }

    public async Task<IEnumerable<RequirementUser>> GetRequirementUsers(Guid requirementId, Guid userId)
    {
        var requirementUsers = await CreateQueryUnsafe()
            .Include(reqUser => reqUser.Requirement)
            .ThenInclude(requirement => requirement!.Event)
            .ThenInclude(meetingEvent => meetingEvent!.Meeting)
            .ThenInclude(meeting => meeting!.MeetingUsers)
            .Where(reqUser => reqUser.RequirementId.Equals(requirementId))
            .ToListAsync();

        if (requirementUsers.FirstOrDefault()?.Requirement?.Event?.Meeting?.MeetingUsers?
            .Any(meetingUser => meetingUser.UserId.Equals(userId)) ?? false)
        {
            return Mapper.Map(requirementUsers);
        }

        return new List<RequirementUser>();
    }
}