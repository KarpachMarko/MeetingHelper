using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    Task RemoveExpiredRefreshTokens(Guid userId);
    
    IAnswerOptionRepository AnswerOptions { get; }
    IBankAccountRepository BankAccounts { get; }
    IEventNavigationRepository EventNavigations { get; }
    IEventRepository Events { get; }
    IEventUserRepository EventUsers { get; }
    IMeetingRepository Meetings { get; }
    IMeetingUserRepository MeetingUsers { get; }
    IMoneyTransferRepository MoneyTransfers { get; }
    IPaymentRepository Payments { get; }
    IQuestionnaireRelationRepository QuestionnaireRelations { get; }
    IQuestionnaireRepository Questionnaires { get; }
    IRequirementOptionRepository RequirementOptions { get; }
    IRequirementRepository Requirements { get; }
    IRequirementUserRepository RequirementUsers { get; }
    IUserRepository Users { get; }
}