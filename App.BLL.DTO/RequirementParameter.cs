using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.BLL.DTO;

public class RequirementParameter : DomainEntityId
{
    [MaxLength(1536)]
    public string ParameterDesc { get; set; } = default!;
    
    public int Priority { get; set; }
    
    public Guid RequirementId { get; set; }
    public Requirement? Requirement { get; set; }
}