using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MeetingUserRepository :
    BaseEntityUserRepository<MeetingUser, Domain.MeetingUser, AppUser, Domain.Identity.AppUser, AppDbContext>,
    IMeetingUserRepository
{
    protected override IQueryable<Domain.MeetingUser> CreateQuery(Guid userId, bool noTracking = true)
    {
        return base.CreateQuery(userId, noTracking)
            .Include(meetingUser => meetingUser.Meeting);
    }

    protected override IQueryable<Domain.MeetingUser> CreateQueryUnsafe(bool noTracking = true)
    {
        return base.CreateQueryUnsafe(noTracking)
            .Include(meetingUser => meetingUser.Meeting);
    }

    public MeetingUserRepository(AppDbContext dbContext, IMapper<MeetingUser, Domain.MeetingUser> mapper) : base(
        dbContext, mapper)
    {
    }
}