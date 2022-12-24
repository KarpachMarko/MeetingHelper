using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class RequirementUserMapper : BaseMapper<RequirementUser, Domain.RequirementUser>
{
    public RequirementUserMapper(IMapper mapper) : base(mapper)
    {
    }
}