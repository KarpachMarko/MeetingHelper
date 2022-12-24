using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace App.Contracts.DAL.Repositories;

public interface IRequirementUserRepository : IEntityUserRepository<RequirementUser, AppUser>, IRequirementUserRepositoryCustom<RequirementUser>
{
    
}

public interface IRequirementUserRepositoryCustom<TEntity>
    where TEntity : IDomainEntityId
{
    
}