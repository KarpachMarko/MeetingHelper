using App.BLL.DTO;
using Base.Contracts.BLL;
using Base.Contracts.Domain;

namespace App.Contracts.BLL.Services;

public interface IRequirementParameterService : IEntityUserDependentService<RequirementParameter>, IRequirementParameterRepositoryCustom<RequirementParameter>
{
    
}

public interface IRequirementParameterRepositoryCustom<TEntity>
    where TEntity : IDomainEntityId
{
    
}