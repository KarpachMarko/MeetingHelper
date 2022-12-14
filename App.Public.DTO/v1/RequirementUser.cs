using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;
using App.Public.DTO.v1.Identity;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.Public.DTO.v1;

public class RequirementUser : DomainEntityId, IDomainEntityUser<AppUser>
{
    public ERequirementRole Role { get; set; }
    public double Proportion { get; set; }
    
    public Guid RequirementId { get; set; }
    [Required]
    public Requirement? Requirement { get; set; }
    public Guid UserId { get; set; }
    [Required]
    public AppUser? User { get; set; }
}