using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO.v1;

public class QuestionnaireRelation : DomainEntityId
{
    public Guid QuestionnaireId { get; set; }
    [Required]
    public Questionnaire? Questionnaire { get; set; }
    
    public Guid? MeetingId { get; set; }
    public Meeting? Meeting { get; set; }
    
    public Guid? EventId { get; set; }
    public Event? Event { get; set; }
    
    public Guid? RequirementId { get; set; }
    public Requirement? Requirement { get; set; }
}