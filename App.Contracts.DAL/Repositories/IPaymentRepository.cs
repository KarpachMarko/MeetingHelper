using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace App.Contracts.DAL.Repositories;

public interface IPaymentRepository : IEntityUserRepository<Payment, AppUser>, IPaymentRepositoryCustom<Payment>
{
    
}

public interface IPaymentRepositoryCustom<TEntity>
    where TEntity : IDomainEntityId
{
    public Task<IEnumerable<TEntity>> GetMeetingPayments(Guid meetingId);
    public Task<IEnumerable<TEntity>> GetRequirementPayments(Guid requirementId);
    public Task<double> GetEventTotalPayments(Guid eventId);
}