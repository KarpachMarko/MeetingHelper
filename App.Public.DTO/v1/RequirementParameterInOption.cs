using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO.v1;

public class RequirementParameterInOption : DomainEntityId
{
    [Required]
    public Guid RequirementOptionId { get; set; }
    
    [Required]
    public Guid RequirementParameterId { get; set; }
}