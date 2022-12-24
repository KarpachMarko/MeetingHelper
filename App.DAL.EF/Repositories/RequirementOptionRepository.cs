using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.Domain.Enums;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class RequirementOptionRepository :
    BaseEntityRepository<RequirementOption, Domain.RequirementOption, AppDbContext>, IRequirementOptionRepository

{
    public RequirementOptionRepository(AppDbContext dbContext,
        IMapper<RequirementOption, Domain.RequirementOption> mapper) : base(dbContext, mapper)
    {
    }

    protected override IQueryable<Domain.RequirementOption> CreateQuery(bool noTracking = true)
    {
        return base.CreateQuery(noTracking)
            .Include(option => option.Requirement);
    }

    public async Task<RequirementOption?> GetSelected(Guid requirementId)
    {
        return Mapper.Map(await CreateQuery()
            .FirstAsync(option => option.RequirementId.Equals(requirementId)
                                  && option.Status.Equals(EOptionStatus.Selected)));
    }
}