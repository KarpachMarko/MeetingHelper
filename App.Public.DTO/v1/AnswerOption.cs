using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO.v1;

public class AnswerOption : DomainEntityId
{
    [MaxLength(128)]
    public string Answer { get; set; } = default!;

    
    public Guid QuestionnaireId { get; set; }
}