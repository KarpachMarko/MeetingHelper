using Base.Contracts.DAL;
using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace Base.Contracts.BLL;

public interface IEntityUserService<TEntity, TUser> : IEntityUserService<TEntity, Guid, TUser>
    where TEntity : class, IDomainEntityId, IDomainEntityUser<TUser>
    where TUser : IdentityUser<Guid>
{
    
}

public interface IEntityUserService<TEntity, TKey, TUser> : IEntityUserRepository<TEntity, TKey, TUser>
    where TEntity : class, IDomainEntityId<TKey>, IDomainEntityUser<TKey, TUser>
    where TKey : IEquatable<TKey>
    where TUser : IdentityUser<TKey> 
{
    
}