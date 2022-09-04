using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class RequirementUserMapper : BaseMapper<RequirementUser, DAL.DTO.RequirementUser>
{
    public RequirementUserMapper(IMapper mapper) : base(mapper)
    {
    }
}