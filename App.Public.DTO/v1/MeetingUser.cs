using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;
using App.Public.DTO.v1.Identity;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.Public.DTO.v1;

public class MeetingUser : DomainEntityId
{
    [Required]
    public EMeetingRole? Role { get; set; }
    
    public Guid MeetingId { get; set; }

    public Guid UserId { get; set; }
}