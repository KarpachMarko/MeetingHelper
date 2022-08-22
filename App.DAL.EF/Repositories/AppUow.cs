using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.EF.Mappers;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class AppUow : BaseUow<AppDbContext>, IAppUnitOfWork
{
    protected readonly IMapper Mapper;

    public AppUow(AppDbContext uowDbContext, IMapper mapper) : base(uowDbContext)
    {
        Mapper = mapper;
    }

    public async Task RemoveExpiredRefreshTokens(Guid userId)
    {
        var tokens = await UowDbContext.RefreshTokens
            .Where(e =>
                e.UserId.Equals(userId) &&
                e.TokenExpirationDateTime < DateTime.UtcNow)
            .ToListAsync();

        UowDbContext.RemoveRange(tokens);
        await SaveChangesAsync();
    }

    private IAnswerOptionRepository? _answerOptions;
    public IAnswerOptionRepository AnswerOptions =>
        _answerOptions ??= new AnswerOptionRepository(UowDbContext, new AnswerOptionMapper(Mapper));

    private IBankAccountRepository? _bankAccounts;
    public IBankAccountRepository BankAccounts =>
        _bankAccounts ??= new BankAccountRepository(UowDbContext, new BankAccountMapper(Mapper));

    private IEventNavigationRepository? _eventNavigations;
    public IEventNavigationRepository EventNavigations =>
        _eventNavigations ??= new EventNavigationRepository(UowDbContext, new EventNavigationMapper(Mapper));

    private IEventRepository? _events;
    public IEventRepository Events => _events ??= new EventRepository(UowDbContext, new EventMapper(Mapper));

    private IEventUserRepository? _eventUsers;
    public IEventUserRepository EventUsers =>
        _eventUsers ??= new EventUserRepository(UowDbContext, new EventUserMapper(Mapper));

    private IMeetingRepository? _meetings;
    public IMeetingRepository Meetings => _meetings ??= new MeetingRepository(UowDbContext, new MeetingMapper(Mapper));

    private IMeetingUserRepository? _meetingUsers;
    public IMeetingUserRepository MeetingUsers =>
        _meetingUsers ??= new MeetingUserRepository(UowDbContext, new MeetingUserMapper(Mapper));

    private IMoneyTransferRepository? _moneyTransfers;
    public IMoneyTransferRepository MoneyTransfers => _moneyTransfers ??=
        new MoneyTransferRepository(UowDbContext, new MoneyTransferMapper(Mapper));

    private IPaymentRepository? _payments;
    public IPaymentRepository Payments => _payments ??= new PaymentRepository(UowDbContext, new PaymentMapper(Mapper));

    private IQuestionnaireRelationRepository? _questionnaireRelations;
    public IQuestionnaireRelationRepository QuestionnaireRelations => _questionnaireRelations ??=
        new QuestionnaireRelationRepository(UowDbContext, new QuestionnaireRelatrionMapper(Mapper));

    private IQuestionnaireRepository? _questionnaires;
    public IQuestionnaireRepository Questionnaires => _questionnaires ??=
        new QuestionnaireRepository(UowDbContext, new QuestionnaireMapper(Mapper));

    private IRequirementOptionRepository? _requirementOptions;
    public IRequirementOptionRepository RequirementOptions => _requirementOptions ??=
        new RequirementOptionRepository(UowDbContext, new RequirementOptionMapper(Mapper));

    private IRequirementRepository? _requirements;
    public IRequirementRepository Requirements =>
        _requirements ??= new RequirementRepository(UowDbContext, new RequirementMapper(Mapper));

    private IRequirementUserRepository? _requirementUsers;
    public IRequirementUserRepository RequirementUsers => _requirementUsers ??=
        new RequirementUserRepository(UowDbContext, new RequirementUserMapper(Mapper));

    private IUserRepository? _users;
    public IUserRepository Users => _users ??= new UserRepository(UowDbContext, new AppUserMapper(Mapper));
}