using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace Base.Contracts.DAL;

public interface IEntityUserRepository<TEntity, TUser> : IEntityUserRepository<TEntity, Guid, TUser>
    where TEntity : class, IDomainEntityId<Guid>, IDomainEntityUser<TUser>
    where TUser : IdentityUser<Guid>
{
}

public interface IEntityUserRepository<TEntity, TKey, TUser>
    where TEntity : class, IDomainEntityId<TKey>, IDomainEntityUser<TKey, TUser>
    where TKey : IEquatable<TKey>
    where TUser : IdentityUser<TKey>
{
    // sync
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity, TKey userId);
    TEntity Remove(TEntity entity);
    TEntity Remove(TKey id, TKey userId);

    TEntity? FirstOrDefault(TKey id, TKey userId, bool noTracking = true);
    IEnumerable<TEntity> GetAll(TKey userId, bool noTracking = true);
    bool Exists(TKey id);
    
    // async
    Task<TEntity> UpdateAsync(TEntity entity, TKey userId);
    
    Task<TEntity?> FirstOrDefaultAsync(TKey id, TKey userId, bool noTracking = true);
    Task<IEnumerable<TEntity>> GetAllAsync(TKey userId, bool noTracking = true);
    Task<bool> ExistsAsync(TKey id);
    Task<TEntity> RemoveAsync(TKey userId, TKey id);
}