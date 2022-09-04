using System.ComponentModel.DataAnnotations;
using App.DAL.DTO.Identity;
using App.Domain.Enums;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.DAL.DTO;

public class EventUser : DomainEntityId, IDomainEntityUser<AppUser>
{
    public EventStatus Status { get; set; }
    
    public Guid EventId { get; set; }
    [Required]
    public Event? Event { get; set; }
    
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
}