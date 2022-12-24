using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class RequirementOptionMapper : BaseMapper<RequirementOption, DAL.DTO.RequirementOption>
{
    public RequirementOptionMapper(IMapper mapper) : base(mapper)
    {
    }
}