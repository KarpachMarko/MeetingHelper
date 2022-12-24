using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class QuestionnaireRelatrionMapper : BaseMapper<QuestionnaireRelation, BLL.DTO.QuestionnaireRelation>
{
    public QuestionnaireRelatrionMapper(IMapper mapper) : base(mapper)
    {
    }
}