using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Requirement : DomainEntityId
{
    [MaxLength(256)]
    public string Title { get; set; } = default!;

    [MaxLength(2512)]
    public string Description { get; set; } = default!;

    public float BudgetPerPerson { get; set; }

    public DateTime DecisionDate { get; set; }

    public DateTime PaymentDate { get; set; }

    public Guid EventId { get; set; }
    
    public Event? Event { get; set; }
    
    public ICollection<QuestionnaireRelation>? QuestionnaireRelations { get; set; }
    
    public ICollection<RequirementUser>? RequirementUsers { get; set; }
    
    public ICollection<Payment>? Payments { get; set; }

    public ICollection<RequirementOption>? RequirementOptions { get; set; }
}