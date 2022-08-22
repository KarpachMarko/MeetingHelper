using App.DAL.DTO;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace App.Contracts.DAL.Repositories;

public interface IRequirementRepository : IEntityUserDependentRepository<Requirement>, IRequirementRepositoryCustom<Requirement>
{
    
}

public interface IRequirementRepositoryCustom<TEntity>
    where TEntity : IDomainEntityId
{
    
}