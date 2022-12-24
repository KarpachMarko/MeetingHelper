using App.DAL.DTO;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace App.Contracts.DAL.Repositories;

public interface IEventRepository : IEntityUserDependentRepository<Event>, IEventRepositoryCustom<Event>
{
    
}

public interface IEventRepositoryCustom<TEntity>
    where TEntity : IDomainEntityId
{
    public Task<IEnumerable<TEntity>> GetMeetingEvents(Guid meetingId, Guid userId);
}