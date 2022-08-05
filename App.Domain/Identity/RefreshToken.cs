using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.Domain.Identity;

public class RefreshToken : DomainEntityId, IDomainEntityUser<AppUser>
{
    [StringLength(36, MinimumLength = 36)] 
    public string Token { get; set; } = Guid.NewGuid().ToString();
    public DateTime TokenExpirationDateTime { get; set; } = DateTime.Now.AddDays(7);
    
    [StringLength(36, MinimumLength = 36)] 
    public string? PreviousToken { get; set; }
    public DateTime? PreviousTokenExpirationDateTime { get; set; }
    
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
}