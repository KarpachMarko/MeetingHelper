using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;

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
}