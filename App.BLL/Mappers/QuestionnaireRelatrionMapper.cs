using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class QuestionnaireRelatrionMapper : BaseMapper<QuestionnaireRelation, DAL.DTO.QuestionnaireRelation>
{
    public QuestionnaireRelatrionMapper(IMapper mapper) : base(mapper)
    {
    }
}