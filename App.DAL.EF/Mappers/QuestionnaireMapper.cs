using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class QuestionnaireMapper : BaseMapper<Questionnaire, Domain.Questionnaire>
{
    public QuestionnaireMapper(IMapper mapper) : base(mapper)
    {
    }
}