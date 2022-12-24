using App.DAL.DTO;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace App.Contracts.DAL.Repositories;

public interface IQuestionnaireRelationRepository : IEntityRepository<QuestionnaireRelation>, IQuestionnaireRelationRepositoryCustom<QuestionnaireRelation>
{
    
}

public interface IQuestionnaireRelationRepositoryCustom<TEntity>
    where TEntity : IDomainEntityId
{
    
}