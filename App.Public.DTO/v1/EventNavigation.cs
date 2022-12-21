using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Public.DTO.v1;

public class EventNavigation : DomainEntityId
{
    [ForeignKey("PreviousEvent")]
    public Guid PreviousEventId { get; set; }
    
    [ForeignKey("NextEvent")]
    public Guid NextEventId { get; set; }
}