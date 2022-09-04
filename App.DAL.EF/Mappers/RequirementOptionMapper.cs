using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class RequirementOptionMapper : BaseMapper<RequirementOption, Domain.RequirementOption>
{
    public RequirementOptionMapper(IMapper mapper) : base(mapper)
    {
    }
}