using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class RequirementParameterMapper : BaseMapper<RequirementParameter, DAL.DTO.RequirementParameter>
{
    public RequirementParameterMapper(IMapper mapper) : base(mapper)
    {
    }
}