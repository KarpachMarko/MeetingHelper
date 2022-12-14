using App.BLL.DTO;
using App.BLL.DTO.Identity;
using AutoMapper;

namespace App.BLL;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<AnswerOption, DAL.DTO.AnswerOption>().ReverseMap();
        CreateMap<BankAccount, DAL.DTO.BankAccount>().ReverseMap();
        CreateMap<Event, DAL.DTO.Event>().ReverseMap();
        CreateMap<EventNavigation, DAL.DTO.EventNavigation>().ReverseMap();
        CreateMap<EventUser, DAL.DTO.EventUser>().ReverseMap();
        CreateMap<Meeting, DAL.DTO.Meeting>().ReverseMap();
        CreateMap<MeetingUser, DAL.DTO.MeetingUser>().ReverseMap();
        CreateMap<MoneyTransfer, DAL.DTO.MoneyTransfer>().ReverseMap();
        CreateMap<Payment, DAL.DTO.Payment>().ReverseMap();
        CreateMap<Questionnaire, DAL.DTO.Questionnaire>().ReverseMap();
        CreateMap<QuestionnaireRelation, DAL.DTO.QuestionnaireRelation>().ReverseMap();
        CreateMap<Requirement, DAL.DTO.Requirement>().ReverseMap();
        CreateMap<RequirementOption, DAL.DTO.RequirementOption>().ReverseMap();
        CreateMap<RequirementUser, DAL.DTO.RequirementUser>().ReverseMap();
        CreateMap<AppUser, DAL.DTO.Identity.AppUser>().ReverseMap();
    }
}