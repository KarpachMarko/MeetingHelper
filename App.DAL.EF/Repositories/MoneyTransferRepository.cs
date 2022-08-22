using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class MoneyTransferRepository : BaseEntityRepository<MoneyTransfer, Domain.MoneyTransfer, AppDbContext>,
    IMoneyTransferRepository
{
    public MoneyTransferRepository(AppDbContext dbContext, IMapper<MoneyTransfer, Domain.MoneyTransfer> mapper) : base(
        dbContext, mapper)
    {
    }
}