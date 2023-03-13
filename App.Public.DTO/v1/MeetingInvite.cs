using App.Domain.Enums;
using App.Public.DTO.v1.Identity;
using Base.Domain;

namespace App.Public.DTO.v1;

public class MeetingInvite : DomainEntityId
{
    public EInviteStatus? Status { get; set; }
    
    public Guid MeetingId { get; set; }
    
    public Guid UserId { get; set; }
}