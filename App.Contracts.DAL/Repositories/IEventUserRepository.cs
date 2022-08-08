using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace App.Contracts.DAL.Repositories;

public interface IEventUserRepository : IEntityUserRepository<EventUser, AppUser>, IEventRepositoryCustom<EventUser>
{
    
}

public interface IEventUserRepositoryCustom<TEntity>
    where TEntity : IDomainEntityId
{
    
}