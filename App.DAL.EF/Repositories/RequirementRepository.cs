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

    public static bool CheckOwnership(Requirement requirement, Guid userId)
    {
        // TODO
        return true;
    }
    
    public async Task<IEnumerable<Requirement>> GetAllInMeeting(Guid meetingId)
    {
        var requirements = await CreateQuery()
            .Include(requirement => requirement.RequirementUsers)
            .Where(requirement => requirement.Event != null && requirement.Event.MeetingId.Equals(meetingId))
            .ToListAsync();

        return Mapper.Map(requirements);
    }
}