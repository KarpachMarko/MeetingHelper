using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class EventUserRepository :
    BaseEntityUserRepository<EventUser, Domain.EventUser, AppUser, Domain.Identity.AppUser, AppDbContext>,
    IEventUserRepository
{
    public EventUserRepository(AppDbContext dbContext, IMapper<EventUser, Domain.EventUser> mapper) : base(dbContext,
        mapper)
    {
    }
}