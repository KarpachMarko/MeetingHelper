using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class RequirementOptionMapper : BaseMapper<RequirementOption, BLL.DTO.RequirementOption>
{
    public RequirementOptionMapper(IMapper mapper) : base(mapper)
    {
    }
}