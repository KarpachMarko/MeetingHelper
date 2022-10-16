using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Base.Resources.EntityCommon;

namespace App.Public.DTO.v1;

public class Requirement : DomainEntityId
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(EntityCommon), Name = "Title")]
    public string Title { get; set; } = default!;

    [MaxLength(2512)]
    [Display(ResourceType = typeof(EntityCommon), Name = "Description")]
    public string Description { get; set; } = default!;

    public double BudgetPerPerson { get; set; }
    public DateTime DecisionDate { get; set; }
    public DateTime PaymentDate { get; set; }

    public Guid EventId { get; set; }
    [Required]
    public Event? Event { get; set; }
}