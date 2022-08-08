using System.ComponentModel.DataAnnotations;
using App.DAL.DTO.Identity;
using App.Domain.Enums;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.DAL.DTO;

public class MeetingUser : DomainEntityId, IDomainEntityUser<AppUser>
{
    public MeetingRole Role { get; set; }
    
    public Guid MeetingId { get; set; }
    [Required]
    public Meeting? Meeting { get; set; }
    
    public Guid UserId { get; set; }
    [Required]
    public AppUser? User { get; set; }
}