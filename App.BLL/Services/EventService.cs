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
}