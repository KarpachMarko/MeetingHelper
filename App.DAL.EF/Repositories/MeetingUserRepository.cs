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
    public MeetingUserRepository(AppDbContext dbContext, IMapper<MeetingUser, Domain.MeetingUser> mapper) : base(
        dbContext, mapper)
    {
    }

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

    public async Task<IEnumerable<MeetingUser>> GetMeetingUsersInMeeting(Guid meetingId, Guid userId)
    {
        var meetingUsers = await CreateQueryUnsafe().Where(meetingUser => meetingUser.MeetingId.Equals(meetingId)).ToListAsync();
        // To get list of meetingUsers you also should be in this meeting. 
        return meetingUsers.Any(meetingUser => meetingUser.UserId.Equals(userId)) ? 
            Mapper.Map(meetingUsers) : new List<MeetingUser>();
    }
}