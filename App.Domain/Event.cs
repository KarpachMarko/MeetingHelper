using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Event : DomainEntityId
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(Base.Resources.EntityCommon.EntityCommon), Name = "Title")]
    public string Title { get; set; } = default!;
    
    [MaxLength(2512)]
    [Display(ResourceType = typeof(Base.Resources.EntityCommon.EntityCommon), Name = "Title")]
    public string Description { get; set; } = default!;
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime DecissionDate { get; set; }
    public double BudgetPerPerson { get; set; }
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }
    public string LocationTitle { get; set; }
    public string LocationLink { get; set; }
    
    public Guid MeetingId { get; set; }
    public Meeting Meeting { get; set; }
    
    public ICollection<EventUser>? EventUsers { get; set; }
    public ICollection<EventNavigation>? EventNavigation { get; set; }
}