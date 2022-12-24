using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class EventService : BaseEntityUserDependentService<Event, DAL.DTO.Event, IEventRepository>, IEventService
{
    public EventService(IEventRepository repository, IMapper<Event, DAL.DTO.Event> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<Event>> GetMeetingEvents(Guid meetingId, Guid userId)
    {
        return Mapper.Map(await Repository.GetMeetingEvents(meetingId, userId));
    }

    public async Task<IEnumerable<Event>> GetFirstMeetingEvents(Guid meetingId, Guid userId,
        IEventNavigationService eventNavigationService)
    {
        var eventNavigations = (await eventNavigationService.GetMeetingEventNavigations(meetingId, userId)).ToList();
        var firstEventNavigations = eventNavigations
            .Select(navigation => navigation.PreviousEvent!)
            .Where(meetingEvent => !eventNavigations.Any(navigation => navigation.NextEventId.Equals(meetingEvent.Id)));

        return firstEventNavigations;
    }

    public async Task<IEnumerable<Event>> GetNextEvents(Guid eventId, Guid userId,
        IEventNavigationService eventNavigationService)
    {
        return (await eventNavigationService.GetNextEventNavigations(eventId, userId)).Select(navigation => navigation.NextEvent!);
    }

    public async Task<IEnumerable<Event>> GetPreviousEvents(Guid eventId, Guid userId,
        IEventNavigationService eventNavigationService)
    {
        return (await eventNavigationService.GetPreviousEventNavigations(eventId, userId)).Select(navigation => navigation.PreviousEvent!);
    }
}