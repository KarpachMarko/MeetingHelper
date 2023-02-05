using App.Public.DTO.v1;
using App.Public.DTO.v1.Identity;
using AutoMapper;

namespace App.Public.DTO;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<AnswerOption, BLL.DTO.AnswerOption>().ReverseMap();
        CreateMap<BankAccount, BLL.DTO.BankAccount>().ReverseMap();
        CreateMap<Event, BLL.DTO.Event>().ReverseMap();
        CreateMap<EventNavigation, BLL.DTO.EventNavigation>().ReverseMap();
        CreateMap<EventUser, BLL.DTO.EventUser>().ReverseMap();
        CreateMap<Meeting, BLL.DTO.Meeting>().ReverseMap();
        CreateMap<MeetingUser, BLL.DTO.MeetingUser>().ReverseMap();
        CreateMap<MeetingInvite, BLL.DTO.MeetingInvite>().ReverseMap();
        CreateMap<MoneyTransfer, BLL.DTO.MoneyTransfer>().ReverseMap();
        CreateMap<Payment, BLL.DTO.Payment>().ReverseMap();
        CreateMap<Questionnaire, BLL.DTO.Questionnaire>().ReverseMap();
        CreateMap<QuestionnaireRelation, BLL.DTO.QuestionnaireRelation>().ReverseMap();
        CreateMap<Requirement, BLL.DTO.Requirement>().ReverseMap();
        CreateMap<RequirementParameter, BLL.DTO.RequirementParameter>().ReverseMap();
        CreateMap<RequirementParameterInOption, BLL.DTO.RequirementParameterInOption>().ReverseMap();
        CreateMap<RequirementOption, BLL.DTO.RequirementOption>().ReverseMap();
        CreateMap<RequirementUser, BLL.DTO.RequirementUser>().ReverseMap();
        CreateMap<AppUser, BLL.DTO.Identity.AppUser>().ReverseMap();
    }
}