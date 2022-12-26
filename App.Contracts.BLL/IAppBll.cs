using App.Contracts.BLL.Services;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

public interface IAppBll : IBll
{
    IAnswerOptionService AnswerOptions { get; }
    IBankAccountService BankAccounts { get; }
    IEventNavigationService EventNavigations { get; }
    IEventService Events { get; }
    IEventUserService EventUsers { get; }
    IMeetingService Meetings { get; }
    IMeetingUserService MeetingUsers { get; }
    IMeetingInviteService MeetingInvites { get; }
    IMoneyTransferService MoneyTransfers { get; }
    IPaymentService Payments { get; }
    IQuestionnaireRelationService QuestionnaireRelations { get; }
    IQuestionnaireService Questionnaires { get; }
    IRequirementOptionService RequirementOptions { get; }
    IRequirementService Requirements { get; }
    IRequirementUserService RequirementUsers { get; }
    IUserService Users { get; }
}