using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class RequirementMapper : BaseMapper<Requirement, BLL.DTO.Requirement>
{
    public RequirementMapper(IMapper mapper) : base(mapper)
    {
    }
}