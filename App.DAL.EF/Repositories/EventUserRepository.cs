using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class EventUserRepository :
    BaseEntityUserRepository<EventUser, Domain.EventUser, AppUser, Domain.Identity.AppUser, AppDbContext>,
    IEventUserRepository
{
    protected override IQueryable<Domain.EventUser> CreateQuery(Guid userId, bool noTracking = true)
    {
        return base.CreateQuery(userId, noTracking)
            .Include(eventUser => eventUser.Event);
    }

    public EventUserRepository(AppDbContext dbContext, IMapper<EventUser, Domain.EventUser> mapper) : base(dbContext,
        mapper)
    {
    }
}