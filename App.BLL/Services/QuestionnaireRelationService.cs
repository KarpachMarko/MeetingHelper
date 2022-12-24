using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class QuestionnaireRelationService :
    BaseEntityService<QuestionnaireRelation, DAL.DTO.QuestionnaireRelation, IQuestionnaireRelationRepository>,
    IQuestionnaireRelationService
{
    public QuestionnaireRelationService(IQuestionnaireRelationRepository repository, IMapper<QuestionnaireRelation, DAL.DTO.QuestionnaireRelation> mapper) : base(repository, mapper)
    {
    }
}