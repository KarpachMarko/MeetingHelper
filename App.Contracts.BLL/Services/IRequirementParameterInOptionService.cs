using App.BLL.DTO;
using Base.Contracts.BLL;
using Base.Contracts.Domain;

namespace App.Contracts.BLL.Services;

public interface IRequirementParameterInOptionService : IEntityService<RequirementParameterInOption>, IRequirementParameterInOptionRepositoryCustom<RequirementParameterInOption>
{
    
}

public interface IRequirementParameterInOptionRepositoryCustom<TEntity>
    where TEntity : IDomainEntityId
{
    
}