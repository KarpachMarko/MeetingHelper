using App.DAL.DTO;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace App.Contracts.DAL.Repositories;

public interface IQuestionnaireRepository : IEntityRepository<Questionnaire>, IQuestionnaireRepositoryCustom<Questionnaire>
{
    
}

public interface IQuestionnaireRepositoryCustom<TEntity>
    where TEntity : IDomainEntityId
{
    
}