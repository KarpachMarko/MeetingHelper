using App.DAL.DTO.Identity;
using App.Domain.Enums;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.DAL.DTO;

public class MeetingInvite : DomainEntityId, IDomainEntityUser<AppUser>
{
    public EInviteStatus? Status { get; set; }
    
    public Guid MeetingId { get; set; }
    public Meeting? Meeting { get; set; }
    
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
}