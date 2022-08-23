using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class RequirementMapper : BaseMapper<Requirement, DAL.DTO.Requirement>
{
    public RequirementMapper(IMapper mapper) : base(mapper)
    {
    }
}