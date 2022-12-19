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

    public async Task<IEnumerable<MoneyTransfer>> GetMeetingMoneyTransfers(Guid meetingId)
    {
        return Mapper.Map(await Repository.GetMeetingMoneyTransfers(meetingId));
    }

    public IEnumerable<MoneyTransfer> GetCloseDebtsTransfers(Dictionary<Guid, double> debts)
    {
        var moneyTransfers = new List<MoneyTransfer>();

        var i = 0;
        while (i < 1000)
        {
            var moneyTransfer = CreateTransfer(debts);
            if (moneyTransfer != null)
            {
                moneyTransfers.Add(moneyTransfer);
            }
            else
            {
                break;
            }
            i++;
        }

        return moneyTransfers;
    }

    private static MoneyTransfer? CreateTransfer(Dictionary<Guid, double> debts)
    {
        foreach (var sender in debts.Keys.Where(user => debts[user] < 0))
        {
            var debtAmount = debts[sender];
            var receiver = GetTransferReceiver(debts, sender);
            
            if (receiver == null || !(debtAmount < 0))
            {
                continue;
            }
            
            var deptAmount = debts[sender];
            var receiverRequire = debts[receiver.Value];
            var amount = receiverRequire >= Math.Abs(deptAmount) ? deptAmount : -1 * receiverRequire;

            debts[sender] -= amount;
            debts[receiver.Value] += amount;
            return new MoneyTransfer
            {
                Amount = amount,
                SenderId = sender,
                ReceiverId = receiver.Value
            };
        }

        return null;
    }

    private static Guid? GetTransferReceiver(Dictionary<Guid, double> debts, Guid sender)
    {
        var debtAmount = debts[sender];
        double? minDiff = null;
        Guid? minDiffReceiver = null;
        foreach (var receiver in debts.Keys.Where(user => user != sender && debts[user] > 0))
        {
            var diff = Math.Abs(debts[receiver] - debtAmount);
            if (minDiff == null)
            {
                minDiff = diff;
                minDiffReceiver = receiver;
            } else if (diff < minDiff)
            {
                minDiff = diff;
                minDiffReceiver = receiver;
            }
        }
        
        return minDiffReceiver;
    }
}