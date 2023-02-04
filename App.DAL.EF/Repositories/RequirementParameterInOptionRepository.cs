using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class RequirementParameterInOptionRepository : BaseEntityRepository<RequirementParameterInOption, Domain.RequirementParameterInOption, AppDbContext>, IRequirementParameterInOptionRepository
{
    public RequirementParameterInOptionRepository(AppDbContext dbContext, IMapper<RequirementParameterInOption, Domain.RequirementParameterInOption> mapper) : base(dbContext, mapper)
    {
    }

    protected override IQueryable<Domain.RequirementParameterInOption> CreateQuery(bool noTracking = true)
    {
        return base.CreateQuery(noTracking)
            .Include(parameterInOption => parameterInOption.RequirementOption)
            .Include(parameterInOption => parameterInOption.RequirementParameter);
    }
}