using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class RequirementParameterInOptionMapper : BaseMapper<RequirementParameterInOption, DAL.DTO.RequirementParameterInOption>
{
    public RequirementParameterInOptionMapper(IMapper mapper) : base(mapper)
    {
    }
}