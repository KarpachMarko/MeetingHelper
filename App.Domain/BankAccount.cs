using System.ComponentModel.DataAnnotations;
using System.Transactions;
using App.Domain.Identity;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.Domain;

public class BankAccount : DomainEntityId, IDomainEntityUser<AppUser>
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(Base.Resources.EntityCommon.EntityCommon), Name = "Title")]
    public string Title { get; set; } = default!;
    
    [MaxLength(256)]
    [Display(ResourceType = typeof(Base.Resources.EntityCommon.EntityCommon), Name = "Description")]
    public string UserName { get; set; } = default!;
    
    [MaxLength(512)]
    public string Number { get; set; } = default!;
    
    public Guid UserId { get; set; }
    [Required]
    public AppUser? User { get; set; }
    
    public ICollection<Transaction>? Transactions { get; set; }
}