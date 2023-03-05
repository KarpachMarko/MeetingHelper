using App.BLL.DTO;
using App.Contracts.DAL.Repositories;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IMoneyTransferService : IEntityService<MoneyTransfer>, IMoneyTransferRepositoryCustom<MoneyTransfer>
{
    public Task<Dictionary<Guid, double>> GetPersonsExpenseInMeeting(
        Guid meetingId,
        IRequirementService requirementService,
        IUserService userService,
        IPaymentService paymentService
    );

    public Task<Dictionary<Guid, double>> GetPersonsPaymentsInMeeting(
        Guid meetingId,
        IRequirementService requirementService,
        IPaymentService paymentService
    );

    public Task<IEnumerable<MoneyTransfer>> GetCloseDebtsTransfers(
        Guid meetingId,
        IRequirementService requirementService,
        IUserService userService,
        IPaymentService paymentService
    );

    public IEnumerable<MoneyTransfer> GetCloseDebtsTransfers(Dictionary<Guid, double> debts);
}