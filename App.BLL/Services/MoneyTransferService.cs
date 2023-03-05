using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;
using Base.Extensions;

namespace App.BLL.Services;

public class MoneyTransferService : BaseEntityService<MoneyTransfer, DAL.DTO.MoneyTransfer, IMoneyTransferRepository>,
    IMoneyTransferService
{
    public MoneyTransferService(IMoneyTransferRepository repository,
        IMapper<MoneyTransfer, DAL.DTO.MoneyTransfer> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<MoneyTransfer>> GetMeetingMoneyTransfers(Guid meetingId)
    {
        return Mapper.Map(await Repository.GetMeetingMoneyTransfers(meetingId));
    }

    public async Task<Dictionary<Guid, double>> GetPersonsExpenseInMeeting(Guid meetingId,
        IRequirementService requirementService, IUserService userService, IPaymentService paymentService)
    {
        var result = new Dictionary<Guid, double>();
        var requirements = await requirementService.GetAllInMeeting(meetingId);

        foreach (var requirement in requirements)
        {
            var usersId = (await userService.GetRequirementUsers(requirement.Id)).Select(user => user.Id);
            foreach (var userId in usersId)
            {
                var requirementUsers = requirement.RequirementUsers;
                if (requirementUsers == null)
                {
                    continue;
                }

                var requirementUser = requirementUsers.First(reqUser =>
                    reqUser.UserId.Equals(userId) && reqUser.RequirementId.Equals(requirement.Id));

                var payed = (await paymentService.GetRequirementPayments(requirement.Id)).Sum(payment =>
                    payment.Amount);

                var expenseAmount = payed / requirementUsers.Count * requirementUser.Proportion * -1;

                result.AddMerge(userId, expenseAmount, MergeFunc);
            }
        }

        var transfers = await GetMeetingMoneyTransfers(meetingId);
        foreach (var transfer in transfers)
        {
            result.AddMerge(transfer.ReceiverId, -transfer.Amount, MergeFunc);
        }

        return result;
    }

    public async Task<Dictionary<Guid, double>> GetPersonsPaymentsInMeeting(Guid meetingId,
        IRequirementService requirementService,
        IPaymentService paymentService)
    {
        var result = new Dictionary<Guid, double>();
        var requirements = await requirementService.GetAllInMeeting(meetingId);

        foreach (var requirement in requirements)
        {
            var payments = (await paymentService.GetRequirementPayments(requirement.Id));
            foreach (var payment in payments)
            {
                var userId = payment.UserId;
                var paymentAmount = payment.Amount;

                result.AddMerge(userId, paymentAmount, MergeFunc);
            }
        }
        
        var transfers = await GetMeetingMoneyTransfers(meetingId);
        foreach (var transfer in transfers)
        {
            result.AddMerge(transfer.SenderId, transfer.Amount, MergeFunc);
        }

        return result;
    }

    public async Task<IEnumerable<MoneyTransfer>> GetCloseDebtsTransfers(Guid meetingId,
        IRequirementService requirementService, IUserService userService, IPaymentService paymentService)
    {
        var personsExpenseInMeeting = await GetPersonsExpenseInMeeting(meetingId, requirementService, userService, paymentService);
        var personsPaymentsInMeeting = await GetPersonsPaymentsInMeeting(meetingId, requirementService, paymentService);
        var debts = personsExpenseInMeeting.Merge(personsPaymentsInMeeting, doubles => doubles.Sum());
        return GetCloseDebtsTransfers(debts);
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
                Amount = Math.Abs(amount),
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
            }
            else if (diff < minDiff)
            {
                minDiff = diff;
                minDiffReceiver = receiver;
            }
        }

        return minDiffReceiver;
    }
    
    private static double MergeFunc(double value, double otherValue) => value + otherValue;
}