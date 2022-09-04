using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.DAL.DTO;

public class AnswerOption : DomainEntityId
{
    [MaxLength(128)]
    public string Answer { get; set; } = default!;

    public Guid QuestionnaireId { get; set; }
    [Required]
    public Questionnaire? Questionnaire { get; set; }
}