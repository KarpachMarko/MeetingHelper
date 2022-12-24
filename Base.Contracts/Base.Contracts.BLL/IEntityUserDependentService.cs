using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace Base.Contracts.BLL;

public interface IEntityUserDependentService<TEntity> : IEntityUserDependentService<TEntity, Guid>
    where TEntity : class, IDomainEntityId
{
    
}

public interface IEntityUserDependentService<TEntity, TKey> : IEntityUserDependentRepository<TEntity, TKey>
    where TEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    
}