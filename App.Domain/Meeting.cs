using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Meeting : DomainEntityId
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(Base.Resources.EntityCommon.EntityCommon), Name = "Title")]
    public string Title { get; set; } = default!;
    
    [MaxLength(2512)]
    [Display(ResourceType = typeof(Base.Resources.EntityCommon.EntityCommon), Name = "Title")]
    public string Description { get; set; } = default!;

    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public double BudgetPerPerson { get; set; }
}