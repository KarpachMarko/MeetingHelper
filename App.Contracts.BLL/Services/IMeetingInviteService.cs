using App.BLL.DTO;
using App.BLL.DTO.Identity;
using App.Contracts.DAL.Repositories;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IMeetingInviteService : IEntityUserService<MeetingInvite, AppUser>, IMeetingInviteRepositoryCustom<MeetingInvite>
{
    public Task<bool> Accept(Guid meetingId, Guid userId, IMeetingUserService meetingUserService);
    public Task<bool> Reject(Guid meetingId, Guid userId);
}