using System.ComponentModel.DataAnnotations;
using App.DAL.DTO.Identity;
using App.Domain.Enums;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.DAL.DTO;

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