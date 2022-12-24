using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class QuestionnaireMapper : BaseMapper<Questionnaire, BLL.DTO.Questionnaire>
{
    public QuestionnaireMapper(IMapper mapper) : base(mapper)
    {
    }
}