using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class EventNavigation : DomainEntityId
{
    [ForeignKey("PreviousEvent")]
    public Guid PreviousEventId { get; set; }
    [Required]
    public Event? PreviousEvent { get; set; }
    
    [ForeignKey("NextEvent")]
    public Guid NextEventId { get; set; }
    [Required]
    public Event? NextEvent { get; set; }
}