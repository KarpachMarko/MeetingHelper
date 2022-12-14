using App.BLL.DTO;
using App.Contracts.DAL.Repositories;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IMoneyTransferService : IEntityService<MoneyTransfer>, IMoneyTransferRepositoryCustom<MoneyTransfer>
{
    public IEnumerable<MoneyTransfer> GetCloseDebtsTransfers(Dictionary<Guid, double> debts);
}