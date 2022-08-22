using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class RequirementOptionRepository :
    BaseEntityRepository<RequirementOption, Domain.RequirementOption, AppDbContext>, IRequirementOptionRepository

{
    public RequirementOptionRepository(AppDbContext dbContext,
        IMapper<RequirementOption, Domain.RequirementOption> mapper) : base(dbContext, mapper)
    {
    }
}