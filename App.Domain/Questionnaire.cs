using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Questionnaire : DomainEntityId
{
    [MaxLength(512)]
    public string Question { get; set; } = default!;

    [MaxLength(128)]
    public string QuestionnaireMsgId { get; set; } = default!;

    public bool Anonymous { get; set; } = false;
    public bool SingleAnswer { get; set; } = true;
    public DateTime ActiveScince { get; set; } = new DateTime();
    public DateTime Deadline { get; set; }
    
    public ICollection<AnswerOption>? AnswerOptinos { get; set; }
    public ICollection<QuestionnaireRelation>? QuestionnaireRelations { get; set; }
}