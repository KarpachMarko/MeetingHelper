using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;
using Base.Domain;
using Base.Resources.EntityCommon;

namespace App.Public.DTO.v1;

public class RequirementOption : DomainEntityId
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(EntityCommon), Name = "Title")]
    public string Title { get; set; } = default!;

    [MaxLength(2512)]
    [Display(ResourceType = typeof(EntityCommon), Name = "Description")]
    public string Description { get; set; } = default!;

    [MaxLength(2512)]
    public string Link { get; set; } = default!;

    public float Price { get; set; }
    
    public EOptionStatus Status { get; set; }
    
    public Guid RequirementId { get; set; }
    [Required]
    public Requirement? Requirement { get; set; }
}