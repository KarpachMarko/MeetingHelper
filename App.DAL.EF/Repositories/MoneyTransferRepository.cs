using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MoneyTransferRepository : BaseEntityRepository<MoneyTransfer, Domain.MoneyTransfer, AppDbContext>,
    IMoneyTransferRepository
{
    public MoneyTransferRepository(AppDbContext dbContext, IMapper<MoneyTransfer, Domain.MoneyTransfer> mapper) : base(
        dbContext, mapper)
    {
    }

    protected override IQueryable<Domain.MoneyTransfer> CreateQuery(bool noTracking = true)
    {
        return base.CreateQuery(noTracking)
            .Include(transfer => transfer.Sender)
            .Include(transfer => transfer.Receiver);
    }

    public async Task<IEnumerable<MoneyTransfer>> GetMeetingMoneyTransfers(Guid meetingId)
    {
        var moneyTransfers = await CreateQuery()
            .Where(transfer => transfer.MeetingId.Equals(meetingId))
            .ToListAsync();

        return Mapper.Map(moneyTransfers);
    }
}