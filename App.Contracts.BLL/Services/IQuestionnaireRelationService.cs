using App.BLL.DTO;
using App.Contracts.DAL.Repositories;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IQuestionnaireRelationService : IEntityService<QuestionnaireRelation>, IQuestionnaireRelationRepositoryCustom<QuestionnaireRelation>
{
    
}