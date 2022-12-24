using App.DAL.DTO.Identity;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace App.Contracts.DAL.Repositories;

public interface IUserRepository : IEntityRepository<AppUser>, IUserRepositoryCustom<AppUser>
{
    
}

public interface IUserRepositoryCustom<TEntity>
    where TEntity : IDomainEntityId
{
    public Task<IEnumerable<TEntity>> GetRequirementUsers(Guid requirementId);
}