using Base.Domain;

namespace App.DAL.DTO;

public class RequirementParameterInOption : DomainEntityId
{
    public Guid RequirementOptionId { get; set; }
    public RequirementOption? RequirementOption { get; set; }
    
    public Guid RequirementParameterId { get; set; }
    public RequirementParameter? RequirementParameter { get; set; }
}