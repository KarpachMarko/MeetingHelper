using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Meeting : DomainEntityId
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(Base.Resources.EntityCommon.EntityCommon), Name = "Title")]
    public string Title { get; set; } = default!;
    
    [MaxLength(2512)]
    [Display(ResourceType = typeof(Base.Resources.EntityCommon.EntityCommon), Name = "Description")]
    public string Description { get; set; } = default!;

    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
    
    public double BudgetPerPerson { get; set; }
    
    public ICollection<MeetingUser>? MeetingUsers { get; set; }
    public ICollection<Event>? Events { get; set; }
    public ICollection<QuestionnaireRelation>? QuestionnaireRelations { get; set; }
}