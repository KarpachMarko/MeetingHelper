using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.EF.Mappers;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class AppUow : BaseUow<AppDbContext>, IAppUnitOfWork
{
    private readonly IMapper _mapper;

    public AppUow(AppDbContext uowDbContext, IMapper mapper) : base(uowDbContext)
    {
        _mapper = mapper;
    }

    private IAnswerOptionRepository? _answerOptions;
    public IAnswerOptionRepository AnswerOptions =>
        _answerOptions ??= new AnswerOptionRepository(UowDbContext, new AnswerOptionMapper(_mapper));

    private IBankAccountRepository? _bankAccounts;
    public IBankAccountRepository BankAccounts =>
        _bankAccounts ??= new BankAccountRepository(UowDbContext, new BankAccountMapper(_mapper));

    private IEventNavigationRepository? _eventNavigations;
    public IEventNavigationRepository EventNavigations =>
        _eventNavigations ??= new EventNavigationRepository(UowDbContext, new EventNavigationMapper(_mapper));

    private IEventRepository? _events;
    public IEventRepository Events => _events ??= new EventRepository(UowDbContext, new EventMapper(_mapper));

    private IEventUserRepository? _eventUsers;
    public IEventUserRepository EventUsers =>
        _eventUsers ??= new EventUserRepository(UowDbContext, new EventUserMapper(_mapper));

    private IMeetingRepository? _meetings;
    public IMeetingRepository Meetings => _meetings ??= new MeetingRepository(UowDbContext, new MeetingMapper(_mapper));

    private IMeetingUserRepository? _meetingUsers;
    public IMeetingUserRepository MeetingUsers =>
        _meetingUsers ??= new MeetingUserRepository(UowDbContext, new MeetingUserMapper(_mapper));

    private IMeetingInviteRepository? _meetingInvites;
    public IMeetingInviteRepository MeetingInvites => _meetingInvites ??=
        new MeetingInviteRepository(UowDbContext, new MeetingInviteMapper(_mapper));

    private IMoneyTransferRepository? _moneyTransfers;
    public IMoneyTransferRepository MoneyTransfers => _moneyTransfers ??=
        new MoneyTransferRepository(UowDbContext, new MoneyTransferMapper(_mapper));

    private IPaymentRepository? _payments;
    public IPaymentRepository Payments => _payments ??= new PaymentRepository(UowDbContext, new PaymentMapper(_mapper));

    private IQuestionnaireRelationRepository? _questionnaireRelations;
    public IQuestionnaireRelationRepository QuestionnaireRelations => _questionnaireRelations ??=
        new QuestionnaireRelationRepository(UowDbContext, new QuestionnaireRelatrionMapper(_mapper));

    private IQuestionnaireRepository? _questionnaires;
    public IQuestionnaireRepository Questionnaires => _questionnaires ??=
        new QuestionnaireRepository(UowDbContext, new QuestionnaireMapper(_mapper));

    private IRequirementOptionRepository? _requirementOptions;
    public IRequirementOptionRepository RequirementOptions => _requirementOptions ??=
        new RequirementOptionRepository(UowDbContext, new RequirementOptionMapper(_mapper));

    private IRequirementRepository? _requirements;
    public IRequirementRepository Requirements =>
        _requirements ??= new RequirementRepository(UowDbContext, new RequirementMapper(_mapper));

    private IRequirementParameterRepository? _requirementParameter;
    public IRequirementParameterRepository RequirementParameters => _requirementParameter ??=
        new RequirementParameterRepository(UowDbContext, new RequirementParameterMapper(_mapper));
    
    private IRequirementParameterInOptionRepository? _requirementParameterInOption;
    public IRequirementParameterInOptionRepository RequirementParameterInOptions =>
        _requirementParameterInOption ??=
            new RequirementParameterInOptionRepository(UowDbContext, new RequirementParameterInOptionMapper(_mapper));

    private IRequirementUserRepository? _requirementUsers;
    public IRequirementUserRepository RequirementUsers => _requirementUsers ??=
        new RequirementUserRepository(UowDbContext, new RequirementUserMapper(_mapper));

    private IUserRepository? _users;
    public IUserRepository Users => _users ??= new UserRepository(UowDbContext, new AppUserMapper(_mapper));
}