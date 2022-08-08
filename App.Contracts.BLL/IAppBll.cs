using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

public interface IAppBLL : IBll
{
    IAnswerOptionService AnswerOptions { get; }
    IBankAccountService BankACcounts { get; }
    IEventNavigationService EventNavigations { get; }
    IEventService Events { get; }
    IEventUserService EventUsers { get; }
    IMeetingService Metings { get; }
    IMeetingUserService MeetingUsers { get; }
    IMoneyTransferService MoneyTransfers { get; }
    IPaymentService Payments { get; }
    IQuestionnaireRelationService QuestionnaireRelations { get; }
    IQuestionnaireService Questionnaires { get; }
    IRequirementOptionService RequirementOptions { get; }
    IRequirementService Requirements { get; }
    IRequirementUserService RequirementUsers { get; }
    IUserService Users { get; }
}