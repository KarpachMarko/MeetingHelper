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
}