using System.ComponentModel.DataAnnotations;
using App.BLL.DTO.Identity;
using App.Domain.Enums;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.BLL.DTO;

public class MeetingUser : DomainEntityId, IDomainEntityUser<AppUser>
{
    [Required]
    public EMeetingRole? Role { get; set; }
    public bool Going { get; set; } = true;

    public Guid MeetingId { get; set; }
    [Required]
    public Meeting? Meeting { get; set; }
    
    public Guid UserId { get; set; }
    [Required]
    public AppUser? User { get; set; }
}