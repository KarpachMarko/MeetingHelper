using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class RequirementUserMapper : BaseMapper<RequirementUser, BLL.DTO.RequirementUser>
{
    public RequirementUserMapper(IMapper mapper) : base(mapper)
    {
    }
}