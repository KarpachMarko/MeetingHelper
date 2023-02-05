using App.DAL.DTO;
using App.DAL.DTO.Identity;
using AutoMapper;

namespace App.DAL.EF;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<AnswerOption, Domain.AnswerOption>().ReverseMap();
        CreateMap<BankAccount, Domain.BankAccount>().ReverseMap();
        CreateMap<Event, Domain.Event>().ReverseMap();
        CreateMap<EventNavigation, Domain.EventNavigation>().ReverseMap();
        CreateMap<EventUser, Domain.EventUser>().ReverseMap();
        CreateMap<Meeting, Domain.Meeting>().ReverseMap();
        CreateMap<MeetingUser, Domain.MeetingUser>().ReverseMap();
        CreateMap<MeetingInvite, Domain.MeetingInvite>().ReverseMap();
        CreateMap<MoneyTransfer, Domain.MoneyTransfer>().ReverseMap();
        CreateMap<Payment, Domain.Payment>().ReverseMap();
        CreateMap<Questionnaire, Domain.Questionnaire>().ReverseMap();
        CreateMap<QuestionnaireRelation, Domain.QuestionnaireRelation>().ReverseMap();
        CreateMap<Requirement, Domain.Requirement>().ReverseMap();
        CreateMap<RequirementParameter, Domain.RequirementParameter>().ReverseMap();
        CreateMap<RequirementParameterInOption, Domain.RequirementParameterInOption>().ReverseMap();
        CreateMap<RequirementOption, Domain.RequirementOption>().ReverseMap();
        CreateMap<RequirementUser, Domain.RequirementUser>().ReverseMap();
        CreateMap<AppUser, Domain.Identity.AppUser>().ReverseMap();
    }
}