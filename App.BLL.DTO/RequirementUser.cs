using System.ComponentModel.DataAnnotations;
using App.BLL.DTO.Identity;
using App.Domain.Enums;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.BLL.DTO;

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