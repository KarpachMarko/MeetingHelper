using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;
using App.Public.DTO.v1.Identity;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.Public.DTO.v1;

public class EventUser : DomainEntityId
{
    public EEventStatus Status { get; set; }
    
    public Guid EventId { get; set; }
    
    public Guid UserId { get; set; }
}