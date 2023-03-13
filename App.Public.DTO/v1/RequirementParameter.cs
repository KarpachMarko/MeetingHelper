using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO.v1;

public class RequirementParameter : DomainEntityId
{
    [Required]
    [MaxLength(1536)]
    public string ParameterDesc { get; set; } = default!;
    
    [Required]
    public int Priority { get; set; }
    
    [Required]
    public Guid RequirementId { get; set; }
}