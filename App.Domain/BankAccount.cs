using System.ComponentModel.DataAnnotations;
using System.Transactions;
using App.Domain.Identity;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.Domain;

public class BankAccount : DomainEntityId, IDomainEntityUser<AppUser>
{
    [MaxLength(256)]
    public string Title { get; set; } = default!;
    
    [MaxLength(256)]
    public string UserName { get; set; } = default!;
    
    [MaxLength(256)]
    public string Number { get; set; } = default!;
    
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
    
    public ICollection<Transaction>? Transactions { get; set; }
}