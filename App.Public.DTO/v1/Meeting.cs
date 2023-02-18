using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Base.Resources.EntityCommon;

namespace App.Public.DTO.v1;

public class Meeting : DomainEntityId
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(EntityCommon), Name = "Title")]
    public string Title { get; set; } = default!;
    
    [MaxLength(2512)]
    [Display(ResourceType = typeof(EntityCommon), Name = "Description")]
    public string Description { get; set; } = default!;

    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; } // TODO: Make Optional
    
    public double BudgetPerPerson { get; set; } // TODO: Make Optional
}