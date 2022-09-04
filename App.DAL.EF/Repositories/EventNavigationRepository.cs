using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class EventNavigationRepository :
    BaseEntityUserDependentRepository<EventNavigation, Domain.EventNavigation, AppDbContext>, IEventNavigationRepository
{
    public EventNavigationRepository(AppDbContext dbContext,
        IMapper<EventNavigation, Domain.EventNavigation> mapper) : base(dbContext, CheckOwnership, mapper)
    {
    }

    public static bool CheckOwnership(EventNavigation eventNav, Guid userId)
    {
        // TODO
        return true;
    }
}