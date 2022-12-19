using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class EventRepository : BaseEntityUserDependentRepository<Event, Domain.Event, AppDbContext>, IEventRepository
{
    public EventRepository(AppDbContext dbContext,
        IMapper<Event, Domain.Event> mapper) : base(dbContext, CheckOwnership, mapper)
    {
    }

    protected override IQueryable<Domain.Event> CreateQuery(bool noTracking = true)
    {
        return base.CreateQuery(noTracking)
            .Include(meetingEvent => meetingEvent.Meeting);
    }

    public static bool CheckOwnership(Event eventObj, Guid userId)
    {
        // TODO
        return true;
    }
}