using App.BLL.DTO;
using App.Contracts.DAL.Repositories;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IEventService : IEntityUserDependentService<Event>, IEventRepositoryCustom<Event>
{
    public Task<IEnumerable<Event>> GetFirstMeetingEvents(Guid meetingId, Guid userId, IEventNavigationService eventNavigationService);
    public Task<IEnumerable<Event>> GetNextEvents(Guid eventId, Guid userId, IEventNavigationService eventNavigationService);
    public Task<IEnumerable<Event>> GetPreviousEvents(Guid eventId, Guid userId, IEventNavigationService eventNavigationService);
}