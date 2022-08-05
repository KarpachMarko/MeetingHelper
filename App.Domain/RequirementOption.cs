using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class RequirementOption : DomainEntityId
{
    [MaxLength(256)]
    public string Title { get; set; } = default!;

    [MaxLength(2512)]
    public string Description { get; set; } = default!;

    [MaxLength(2512)]
    public string Link { get; set; } = default!;

    public float Price { get; set; }
    
    public Guid RequirementId { get; set; }
    
    public Requirement? Requirement { get; set; }
}