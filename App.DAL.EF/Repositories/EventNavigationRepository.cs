using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class EventNavigationRepository :
    BaseEntityUserDependentRepository<EventNavigation, Domain.EventNavigation, AppDbContext>, IEventNavigationRepository
{
    public EventNavigationRepository(AppDbContext dbContext,
        IMapper<EventNavigation, Domain.EventNavigation> mapper) : base(dbContext, CheckOwnership, mapper)
    {
    }

    protected override IQueryable<Domain.EventNavigation> CreateQuery(bool noTracking = true)
    {
        return base.CreateQuery(noTracking)
            .Include(navigation => navigation.NextEvent)
            .Include(navigation => navigation.PreviousEvent);
    }

    public static bool CheckOwnership(EventNavigation eventNav, Guid userId)
    {
        // TODO
        return true;
    }
}