using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;
using App.Public.DTO.v1.Identity;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.Public.DTO.v1;

public class EventUser : DomainEntityId, IDomainEntityUser<AppUser>
{
    public EventStatus Status { get; set; }
    
    public Guid EventId { get; set; }
    [Required]
    public Event? Event { get; set; }
    
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
}