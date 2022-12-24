using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Base.Resources.EntityCommon;

namespace App.Public.DTO.v1;

public class Event : DomainEntityId
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(EntityCommon), Name = "Title")]
    public string Title { get; set; } = default!;
    
    [MaxLength(2512)]
    [Display(ResourceType = typeof(EntityCommon), Name = "Description")]
    public string Description { get; set; } = default!;
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime? DecisionDate { get; set; }
    public double? BudgetPerPerson { get; set; }
    public int? MinPersonCount { get; set; }
    public int? MaxPersonCount { get; set; }

    [MaxLength(256)]
    public string LocationTitle { get; set; } = default!;
    [MaxLength(2512)]
    public string? LocationLink { get; set; } = default!;
    
    public Guid MeetingId { get; set; }
}