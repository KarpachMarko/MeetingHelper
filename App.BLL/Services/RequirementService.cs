using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;
using Base.Extensions;

namespace App.BLL.Services;

public class RequirementService :
    BaseEntityUserDependentService<Requirement, DAL.DTO.Requirement, IRequirementRepository>,
    IRequirementService
{
    public RequirementService(IRequirementRepository repository, IMapper<Requirement, DAL.DTO.Requirement> mapper) :
        base(repository, mapper)
    {
    }

    public async Task<IEnumerable<Requirement>> GetAllInMeeting(Guid meetingId)
    {
        return Mapper.Map(await Repository.GetAllInMeeting(meetingId));
    }

    public async Task<Dictionary<Guid, double>> GetPersonsExpenseInMeeting(Guid meetingId, IUserService userService, IPaymentService paymentService)
    {
        var result = new Dictionary<Guid, double>();
        var requirements = await GetAllInMeeting(meetingId);

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

                var payed = (await paymentService.GetRequirementPayments(requirement.Id)).Sum(payment => payment.Amount);

                var expenseAmount = payed / requirementUsers.Count * requirementUser.Proportion * -1;

                result.AddMerge(userId, expenseAmount, (value, otherValue) => value + otherValue);
            }
        }

        return result;
    }
    
    public async Task<Dictionary<Guid, double>> GetPersonsPaymentsInMeeting(Guid meetingId, IPaymentService paymentService)
    {
        var result = new Dictionary<Guid, double>();
        var requirements = await GetAllInMeeting(meetingId);

        foreach (var requirement in requirements)
        {
            var payments = (await paymentService.GetRequirementPayments(requirement.Id));
            foreach (var payment in payments)
            {
                var userId = payment.UserId;
                var paymentAmount = payment.Amount;

                result.AddMerge(userId, paymentAmount, (value, otherValue) => value + otherValue);
            }
        }

        return result;
    }
}