using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class RequirementParameterMapper : BaseMapper<RequirementParameter, BLL.DTO.RequirementParameter>
{
    public RequirementParameterMapper(IMapper mapper) : base(mapper)
    {
    }
}