using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class QuestionnaireRelatrionMapper : BaseMapper<QuestionnaireRelation, Domain.QuestionnaireRelation>
{
    public QuestionnaireRelatrionMapper(IMapper mapper) : base(mapper)
    {
    }
}