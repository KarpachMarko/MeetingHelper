using App.DAL.DTO;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace App.Contracts.DAL.Repositories;

public interface IMoneyTransferRepository : IEntityRepository<MoneyTransfer>, IMoneyTransferRepositoryCustom<MoneyTransfer>
{
    
}

public interface IMoneyTransferRepositoryCustom<TEntity>
    where TEntity : IDomainEntityId
{
    public Task<IEnumerable<TEntity>> GetMeetingMoneyTransfers(Guid meetingId);
}