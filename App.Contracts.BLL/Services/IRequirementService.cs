using App.BLL.DTO;
using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IRequirementService : IEntityUserDependentRepository<Requirement>, IRequirementRepositoryCustom<Requirement>
{
    public Task<Dictionary<Guid, double>> GetPersonsExpenseInMeeting(Guid meetingId, IUserService userService,
        IRequirementOptionService optionService);

    public Task<Dictionary<Guid, double>> GetPersonsPaymentsInMeeting(Guid meetingId, IPaymentService paymentService);
}