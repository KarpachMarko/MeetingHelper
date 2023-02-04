using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class RequirementParameterInOptionMapper : BaseMapper<RequirementParameterInOption, Domain.RequirementParameterInOption>
{
    public RequirementParameterInOptionMapper(IMapper mapper) : base(mapper)
    {
    }
}