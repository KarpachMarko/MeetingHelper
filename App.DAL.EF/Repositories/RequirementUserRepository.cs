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
}