using App.DAL.DTO;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace App.Contracts.DAL.Repositories;

public interface IAnswerOptionRepository : IEntityRepository<AnswerOption>, IAnswerOptionRepositoryCustom<AnswerOption>
{
    
}

public interface IAnswerOptionRepositoryCustom<TEntity>
    where TEntity : IDomainEntityId
{
    
}