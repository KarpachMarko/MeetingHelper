using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using App.Domain.Enums;
using Base.Contracts;
using Base.Contracts.DAL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MeetingInviteRepository : BaseEntityUserRepository<MeetingInvite, Domain.MeetingInvite, AppUser,
        Domain.Identity.AppUser, AppDbContext>, IMeetingInviteRepository
{
    public MeetingInviteRepository(AppDbContext dbContext, IMapper<MeetingInvite, Domain.MeetingInvite> mapper) : base(
        dbContext, mapper)
    {
    }

    protected override IQueryable<Domain.MeetingInvite> CreateQuery(Guid userId, bool noTracking = true)
    {
        return base.CreateQuery(userId, noTracking)
        .Include(invite => invite.Meeting);
    }

    protected override IQueryable<Domain.MeetingInvite> CreateQueryUnsafe(bool noTracking = true)
    {
        return base.CreateQueryUnsafe(noTracking)
        .Include(invite => invite.Meeting);
    }

    public override MeetingInvite Add(MeetingInvite entity)
    {
        var invite = CreateQueryUnsafe().FirstOrDefault(invite => invite.UserId.Equals(entity.UserId));
        return invite != null ? Mapper.Map(invite)! : base.Add(entity);
    }

    public async Task<IEnumerable<MeetingInvite>> GetUnansweredInvites(Guid userId)
    {
        return Mapper.Map(await CreateQuery(userId).Where(invite => invite.Status.Equals(null)).ToListAsync());
    }
}