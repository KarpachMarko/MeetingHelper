using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MeetingRepository : BaseEntityUserDependentRepository<Meeting, Domain.Meeting, AppDbContext>,
    IMeetingRepository
{
    public MeetingRepository(AppDbContext dbContext,
        IMapper<Meeting, Domain.Meeting> mapper) : base(dbContext, CheckOwnership, mapper)
    {
    }

    protected override IQueryable<Domain.Meeting> CreateQuery(bool noTracking = true)
    {
        return base.CreateQuery(noTracking)
            .Include(meeting => meeting.MeetingUsers);
    }

    public static bool CheckOwnership(Meeting meeting, Guid userId)
    {
        return meeting.MeetingUsers?.Any(meetingUser => meetingUser.UserId.Equals(userId)) ?? false;
    }
}