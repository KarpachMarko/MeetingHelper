using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Base.Resources.EntityCommon;

namespace App.Domain;

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
    
    public ICollection<RequirementUser>? RequirementUsers { get; set; }
    public ICollection<Payment>? Payments { get; set; }
    public ICollection<RequirementOption>? RequirementOptions { get; set; }
    public ICollection<QuestionnaireRelation>? QuestionnaireRelations { get; set; }
}