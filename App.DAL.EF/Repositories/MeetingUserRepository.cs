using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class MeetingUserRepository :
    BaseEntityUserRepository<MeetingUser, Domain.MeetingUser, AppUser, Domain.Identity.AppUser, AppDbContext>,
    IMeetingUserRepository
{
    public MeetingUserRepository(AppDbContext dbContext, IMapper<MeetingUser, Domain.MeetingUser> mapper) : base(
        dbContext, mapper)
    {
    }
}