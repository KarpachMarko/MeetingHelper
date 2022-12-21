using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace App.Contracts.DAL.Repositories;

public interface IMeetingUserRepository : IEntityUserRepository<MeetingUser, AppUser>, IMeetingUserRepositoryCustom<MeetingUser>
{
    
}

public interface IMeetingUserRepositoryCustom<TEntity>
    where TEntity : IDomainEntityId
{
    public Task<IEnumerable<TEntity>> GetMeetingUsersInMeeting(Guid meetingId, Guid userId);
}