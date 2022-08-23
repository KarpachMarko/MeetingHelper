using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class MoneyTransferService : BaseEntityService<MoneyTransfer, DAL.DTO.MoneyTransfer, IMoneyTransferRepository>,
    IMoneyTransferService
{
    public MoneyTransferService(IMoneyTransferRepository repository, IMapper<MoneyTransfer, DAL.DTO.MoneyTransfer> mapper) : base(repository, mapper)
    {
    }
}