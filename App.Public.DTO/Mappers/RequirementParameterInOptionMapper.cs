using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class RequirementParameterInOptionMapper : BaseMapper<RequirementParameterInOption, BLL.DTO.RequirementParameterInOption>
{
    public RequirementParameterInOptionMapper(IMapper mapper) : base(mapper)
    {
    }
}