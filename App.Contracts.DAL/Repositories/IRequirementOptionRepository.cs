using App.DAL.DTO;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace App.Contracts.DAL.Repositories;

public interface IRequirementOptionRepository : IEntityRepository<RequirementOption>, IRequirementOptionRepositoryCustom<RequirementOption>
{
    
}

public interface IRequirementOptionRepositoryCustom<TEntity>
    where TEntity : IDomainEntityId
{
    
}