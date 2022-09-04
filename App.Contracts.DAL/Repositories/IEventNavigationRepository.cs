using App.DAL.DTO;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace App.Contracts.DAL.Repositories;

public interface IEventNavigationRepository : IEntityUserDependentRepository<EventNavigation>, IEventNavigationRepositoryCustom<EventNavigation>
{
    
}

public interface IEventNavigationRepositoryCustom<TEntity>
    where TEntity : IDomainEntityId
{
    
}