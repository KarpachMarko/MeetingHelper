using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace Base.Domain.Identity;

public abstract class BaseRole : BaseRole<Guid>
{
    
}

public abstract class BaseRole<TKey> : IdentityRole<TKey>, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    public BaseRole() : base()
    {
    }
    
    public BaseRole(string roleName) : base(roleName)
    {
    }
}