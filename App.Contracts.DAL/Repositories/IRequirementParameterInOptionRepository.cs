using App.DAL.DTO;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace App.Contracts.DAL.Repositories;

public interface IRequirementParameterInOptionRepository : IEntityRepository<RequirementParameterInOption>, IRequirementParameterInOptionRepositoryCustom<RequirementParameterInOption>
{
    
}

public interface IRequirementParameterInOptionRepositoryCustom<TEntity>
    where TEntity : IDomainEntityId
{
    
}