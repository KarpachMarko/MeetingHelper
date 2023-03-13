using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class QuestionnaireRelationMapper : BaseMapper<QuestionnaireRelation, DAL.DTO.QuestionnaireRelation>
{
    public QuestionnaireRelationMapper(IMapper mapper) : base(mapper)
    {
    }
}