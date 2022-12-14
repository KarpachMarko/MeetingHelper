using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;
using App.Domain.Identity;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.Domain;

public class EventUser : DomainEntityId, IDomainEntityUser<AppUser>
{
    public EEventStatus Status { get; set; }
    
    public Guid EventId { get; set; }
    public Event? Event { get; set; }
    
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
}