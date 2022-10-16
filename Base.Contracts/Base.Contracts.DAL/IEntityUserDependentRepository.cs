using Base.Contracts.Domain;

namespace Base.Contracts.DAL;

public interface IEntityUserDependentRepository<TEntity> : IEntityUserDependentRepository<TEntity, Guid>
    where TEntity : class, IDomainEntityId<Guid>
{
    
}

public interface IEntityUserDependentRepository<TEntity, TKey>
    where TEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    // sync
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity Remove(TEntity entity);
    TEntity Remove(TKey id, TKey userId);

    TEntity? FirstOrDefault(TKey id, TKey userId, bool noTracking = true);
    IEnumerable<TEntity> GetAll(TKey userId, bool noTracking = true);
    bool Exists(TKey id);
    
    // async
    Task<TEntity> UpdateAsync(TEntity entity);
    
    Task<TEntity?> FirstOrDefaultAsync(TKey id, TKey userId, bool noTracking = true);
    Task<IEnumerable<TEntity>> GetAllAsync(TKey userId, bool noTracking = true);
    Task<bool> ExistsAsync(TKey id);
    Task<TEntity> RemoveAsync(TKey id, TKey userId);
}