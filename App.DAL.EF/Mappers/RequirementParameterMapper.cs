using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class RequirementParameterMapper : BaseMapper<RequirementParameter, Domain.RequirementParameter>
{
    public RequirementParameterMapper(IMapper mapper) : base(mapper)
    {
    }
}