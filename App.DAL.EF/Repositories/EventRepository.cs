using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class EventRepository : BaseEntityUserDependentRepository<Event, Domain.Event, AppDbContext>, IEventRepository
{
    public EventRepository(AppDbContext dbContext,
        IMapper<Event, Domain.Event> mapper) : base(dbContext, CheckOwnership, mapper)
    {
    }
    
    public static bool CheckOwnership(Event eventObj, Guid userId)
    {
        // TODO
        return true;
    }
}