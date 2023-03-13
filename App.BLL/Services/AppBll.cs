using App.BLL.Mappers;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class AppBll : BaseBll<IAppUnitOfWork>, IAppBll
{
    protected readonly IAppUnitOfWork UnitOfWork;
    private readonly IMapper _mapper;

    public AppBll(IAppUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public override int SaveChanges()
    {
        return UnitOfWork.SaveChanges();
    }

    public override Task<int> SaveChangesAsync()
    {
        return UnitOfWork.SaveChangesAsync();
    }

    private IAnswerOptionService? _answerOptions;
    public IAnswerOptionService AnswerOptions =>
        _answerOptions ??= new AnswerOptionService(UnitOfWork.AnswerOptions, new AnswerOptionMapper(_mapper));

    private IBankAccountService? _bankAccounts;
    public IBankAccountService BankAccounts =>
        _bankAccounts ??= new BankAccountService(UnitOfWork.BankAccounts, new BankAccountMapper(_mapper));

    private IEventNavigationService? _eventNavigations;
    public IEventNavigationService EventNavigations =>
        _eventNavigations ??= new EventNavigationService(UnitOfWork.EventNavigations, new EventNavigationMapper(_mapper));

    private IEventService? _events;
    public IEventService Events => _events ??= new EventService(UnitOfWork.Events, new EventMapper(_mapper));

    private IEventUserService? _eventUsers;
    public IEventUserService EventUsers =>
        _eventUsers ??= new EventUserService(UnitOfWork.EventUsers, new EventUserMapper(_mapper));

    private IMeetingService? _meetings;
    public IMeetingService Meetings => _meetings ??= new MeetingService(UnitOfWork.Meetings, new MeetingMapper(_mapper));

    private IMeetingUserService? _meetingUsers;
    public IMeetingUserService MeetingUsers =>
        _meetingUsers ??= new MeetingUserService(UnitOfWork.MeetingUsers, new MeetingUserMapper(_mapper));

    private IMeetingInviteService? _meetingInvites;
    public IMeetingInviteService MeetingInvites => _meetingInvites ??= new MeetingInviteService(UnitOfWork.MeetingInvites, new MeetingInviteMapper(_mapper));

    private IMoneyTransferService? _moneyTransfers;
    public IMoneyTransferService MoneyTransfers => _moneyTransfers ??=
        new MoneyTransferService(UnitOfWork.MoneyTransfers, new MoneyTransferMapper(_mapper));

    private IPaymentService? _payments;
    public IPaymentService Payments => _payments ??= new PaymentService(UnitOfWork.Payments, new PaymentMapper(_mapper));

    private IQuestionnaireRelationService? _questionnaireRelations;
    public IQuestionnaireRelationService QuestionnaireRelations => _questionnaireRelations ??=
        new QuestionnaireRelationService(UnitOfWork.QuestionnaireRelations, new QuestionnaireRelationMapper(_mapper));

    private IQuestionnaireService? _questionnaires;
    public IQuestionnaireService Questionnaires => _questionnaires ??=
        new QuestionnaireService(UnitOfWork.Questionnaires, new QuestionnaireMapper(_mapper));

    private IRequirementOptionService? _requirementOptions;
    public IRequirementOptionService RequirementOptions => _requirementOptions ??=
        new RequirementOptionService(UnitOfWork.RequirementOptions, new RequirementOptionMapper(_mapper));

    private IRequirementService? _requirements;
    public IRequirementService Requirements =>
        _requirements ??= new RequirementService(UnitOfWork.Requirements, new RequirementMapper(_mapper));

    private IRequirementParameterService? _requirementParameter;
    public IRequirementParameterService RequirementParameters => _requirementParameter ??=
        new RequirementParameterService(UnitOfWork.RequirementParameters, new RequirementParameterMapper(_mapper));

    private IRequirementParameterInOptionService? _requirementParameterInOption;
    public IRequirementParameterInOptionService RequirementsParameterInOptions => _requirementParameterInOption ??=
        new RequirementParameterInOptionService(UnitOfWork.RequirementParameterInOptions,
            new RequirementParameterInOptionMapper(_mapper));

    private IRequirementUserService? _requirementUsers;
    public IRequirementUserService RequirementUsers => _requirementUsers ??=
        new RequirementUserService(UnitOfWork.RequirementUsers, new RequirementUserMapper(_mapper));

    private IUserService? _users;
    public IUserService Users => _users ??= new UserService(UnitOfWork.Users, new AppUserMapper(_mapper));
}