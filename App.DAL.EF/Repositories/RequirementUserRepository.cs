using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class RequirementUserRepository :
    BaseEntityUserRepository<RequirementUser, Domain.RequirementUser, AppUser, Domain.Identity.AppUser, AppDbContext>,
    IRequirementUserRepository
{
    public RequirementUserRepository(AppDbContext dbContext, IMapper<RequirementUser, Domain.RequirementUser> mapper) :
        base(dbContext, mapper)
    {
    }
}