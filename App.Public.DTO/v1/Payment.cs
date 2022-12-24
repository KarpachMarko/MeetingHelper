using System.ComponentModel.DataAnnotations;
using App.Public.DTO.v1.Identity;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Payment : DomainEntityId
{
    public double Amount { get; set; }
    public DateTime Timestamp { get; set; }

    public Guid RequirementId { get; set; }
    
    public Guid UserId { get; set; }
}