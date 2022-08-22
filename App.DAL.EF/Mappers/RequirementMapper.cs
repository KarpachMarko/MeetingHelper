using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class RequirementMapper : BaseMapper<Requirement, Domain.Requirement>
{
    public RequirementMapper(IMapper mapper) : base(mapper)
    {
    }
}