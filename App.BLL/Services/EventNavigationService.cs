using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class EventNavigationService :
    BaseEntityUserDependentService<EventNavigation, DAL.DTO.EventNavigation, IEventNavigationRepository>, IEventNavigationService
{
    public EventNavigationService(IEventNavigationRepository repository, IMapper<EventNavigation, DAL.DTO.EventNavigation> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<EventNavigation>> GetMeetingEventNavigations(Guid meetingId, Guid userId)
    {
        return Mapper.Map(await Repository.GetMeetingEventNavigations(meetingId, userId));
    }

    public async Task<IEnumerable<EventNavigation>> GetNextEventNavigations(Guid eventId, Guid userId)
    {
        return Mapper.Map(await Repository.GetNextEventNavigations(eventId, userId));
    }

    public async Task<IEnumerable<EventNavigation>> GetPreviousEventNavigations(Guid eventId, Guid userId)
    {
        return Mapper.Map(await Repository.GetPreviousEventNavigations(eventId, userId));
    }
}