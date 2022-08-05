using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;
using App.Domain.Identity;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.Domain;

public class RequirementUser : DomainEntityId, IDomainEntityUser<AppUser>
{
    public RequirementRole Role { get; set; }
    public double Proportion { get; set; }
    
    public Guid RequirementId { get; set; }
    [Required]
    public Requirement? Requirement { get; set; }
    public Guid UserId { get; set; }
    [Required]
    public AppUser? User { get; set; }
}