using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class RequirementParameterRepository : BaseEntityUserDependentRepository<RequirementParameter, Domain.RequirementParameter, AppDbContext>, IRequirementParameterRepository
{
    public RequirementParameterRepository(AppDbContext dbContext, IMapper<RequirementParameter, Domain.RequirementParameter> mapper) : base(dbContext, CheckOwnership, mapper)
    {
    }
    
    public static bool CheckOwnership(RequirementParameter requirementParameter, Guid userId)
    {
        return requirementParameter.Requirement?.Event?.Meeting?.MeetingUsers?.Any(meetingUser => meetingUser.UserId.Equals(userId)) ?? false;
    }

    protected override IQueryable<Domain.RequirementParameter> CreateQuery(bool noTracking = true)
    {
        return base.CreateQuery(noTracking)
            .Include(parameter => parameter.Requirement)
            .ThenInclude(requirement => requirement!.Event)
            .ThenInclude(meetingEvent => meetingEvent!.Meeting)
            .ThenInclude(meeting => meeting!.MeetingUsers);
    }

    public async Task<IEnumerable<RequirementParameter>> GetRequirementParameters(Guid requirementId, Guid userId)
    {
        var requirementParameters = await CreateQuery()
            .Where(parameter => parameter.RequirementId.Equals(requirementId))
            .ToListAsync();

        return Mapper.Map(requirementParameters).Where(parameter => CheckOwnership(parameter, userId));
    }

    public async Task SetRequirementParameters(Guid requirementId, IEnumerable<RequirementParameter> parameters, Guid userId)
    {
        var requirementParameters = await CreateQuery()
            .Where(parameter => parameter.RequirementId.Equals(requirementId))
            .ToListAsync();
        
        Mapper.Map(requirementParameters)
            .Where(parameter => CheckOwnership(parameter, userId))
            .ToList()
            .ForEach(parameter => Remove(parameter));
        
        parameters
            .ToList()
            .ForEach(parameter => Add(parameter));
    }
}