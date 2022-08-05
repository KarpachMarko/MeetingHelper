using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;
using Base.Resources.EntityCommon;

namespace Base.Domain;

public abstract class DomainEntityMetaId : DomainEntityMetaId<Guid>, IDomainEntityId
{
    
}

public abstract class DomainEntityMetaId<TKey> : DomainEntityId<TKey>, IDomainEntityMeta 
    where TKey : IEquatable<TKey>
{
    [MaxLength(32)]
    [Display(ResourceType = typeof(EntityCommon), Name = "CreatedBy")]
    public string? CreatedBy { get; set; }
    
    [Display(ResourceType = typeof(EntityCommon), Name = "CreatedAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    
    [MaxLength(32)]
    [Display(ResourceType = typeof(EntityCommon), Name = "UpdatedBy")]
    public string? UpdatedBy { get; set; }
    
    [Display(ResourceType = typeof(EntityCommon), Name = "UpdatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}