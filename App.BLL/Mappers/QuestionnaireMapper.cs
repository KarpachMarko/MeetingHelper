using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class QuestionnaireMapper : BaseMapper<Questionnaire, DAL.DTO.Questionnaire>
{
    public QuestionnaireMapper(IMapper mapper) : base(mapper)
    {
    }
}