using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace App.Contracts.DAL.Repositories;

public interface IMeetingInviteRepository : IEntityUserRepository<MeetingInvite, AppUser>, IMeetingInviteRepositoryCustom<MeetingInvite>
{
}

public interface IMeetingInviteRepositoryCustom<TEntity>
    where TEntity : IDomainEntityId
{
    Task<IEnumerable<TEntity>> GetUnansweredInvites(Guid userId);
}